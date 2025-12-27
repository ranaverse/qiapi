namespace qiapi.Controllers
{
    using Fido2NetLib;
    using Fido2NetLib.Objects;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using QI.Infrastructure;
    using qiapi.Models;
    using System.Buffers.Text;
    using System.Text;
    using qiapi.Helpers;
    using Fido2NetLib.Objects;


    [ApiController]
    [Route("webauthn")]
    public class WebAuthnController : ControllerBase
    {
        private readonly Fido2 fido2qi;
        private readonly ApplicationDbContext _db;

        public WebAuthnController(IOptions<Fido2Configuration> fido2Config, ApplicationDbContext db)
        {
            fido2qi = new Fido2(fido2Config.Value);
            _db = db;
        }

        private static readonly Dictionary<string, CredentialCreateOptions> pendingRegistrations = new();
        private static readonly Dictionary<string, AssertionOptions> pendingAssertions = new();

        [HttpPost("register/begin")]
        public ActionResult BeginRegistration([FromBody] string username)
        {
            var user = new Fido2User
            {
                DisplayName = username,
                Name = username,
                Id = Encoding.UTF8.GetBytes(username)
            };


            var requestParams = new RequestNewCredentialParams
            {
                User = user,
                AttestationPreference = AttestationConveyancePreference.None,
                AuthenticatorSelection = new AuthenticatorSelection
                {
                    RequireResidentKey = false,
                    UserVerification = UserVerificationRequirement.Preferred
                },
                ExcludeCredentials = new List<PublicKeyCredentialDescriptor>(),
                PubKeyCredParams = new List<PubKeyCredParam>
                {
                    new PubKeyCredParam(COSE.Algorithm.ES256),
                    new PubKeyCredParam(COSE.Algorithm.RS256)
                },
                Extensions = null
            };


            var options = fido2qi.RequestNewCredential(requestParams);
            var encodedId = EncodingHelper.Base64UrlEncode(user.Id);
            pendingRegistrations[username] = options;




            return new JsonResult(options);
        }

        [HttpPost("register/complete")]
        public async Task<IActionResult> CompleteRegistration([FromBody] AuthenticatorAttestationRawResponse attestation, [FromQuery] string username)
        {
            try
            {
                Console.WriteLine("🔐 REGISTER/COMPLETE HIT");
                Console.WriteLine("Payload ID: " + attestation.Id);

                if (!pendingRegistrations.TryGetValue(username, out var originalOptions))
                {
                    return BadRequest("No pending registration found.");
                }

                var result = await fido2qi.MakeNewCredentialAsync(new MakeNewCredentialParams
                {
                    AttestationResponse = attestation,
                    OriginalOptions = originalOptions,
                    IsCredentialIdUniqueToUserCallback = (credentialId, user) => Task.FromResult(true)

                });

                if (result == null)
                {
                    return BadRequest("Credential creation failed.");
                }

                var alreadyExists = await _db.AuthCredential
                    .AnyAsync(c => c.CredentialId == Convert.ToBase64String(result.Id));



                if (alreadyExists)
                {
                    return Conflict("Credential already registered.");
                }

                var credential = new AuthCredential
                {
                    CredentialId = Convert.ToBase64String(result.Id),

                    PublicKey = Convert.ToBase64String(result.PublicKey),
                    SignatureCounter = result.SignCount,
                    UserId = Convert.ToBase64String(result.User.Id),
                    CreatedAt = DateTime.UtcNow
                };


                _db.AuthCredential.Add(credential);
                await _db.SaveChangesAsync();
                Console.WriteLine("🔐 Register/complete hit");

            }
            catch (Exception ex) 
            {
                Console.WriteLine("❌ ERROR in CompleteRegistration: " + ex.Message);
                return BadRequest("Registration failed.");
            }
            var saved = await _db.SaveChangesAsync();
            Console.WriteLine("💾 Rows saved: " + saved);


            return Ok(new { message = "Registration complete." });
        }



        [HttpPost("assertion/options")]
        public ActionResult BeginAssertion([FromBody] string username)
        {
            var options = fido2qi.GetAssertionOptions(
                new List<PublicKeyCredentialDescriptor>(),
                UserVerificationRequirement.Preferred
            );

            pendingAssertions[username] = options;

            return new JsonResult(options);
        }

        [HttpPost("assertion/complete")]
        public async Task<IActionResult> CompleteAssertion([FromBody] AuthenticatorAssertionRawResponse assertion)
        {
            // Assuming you're still using the username as the key
            var key = assertion.Id; // or wherever you're storing it — adjust accordingly

            if (!pendingAssertions.TryGetValue(key, out var originalOptions))
            {
                return BadRequest("No pending assertion found.");
            }

            // Fetch credential from DB
            var stored = await _db.AuthCredential
                .Where(c => c.CredentialId == assertion.Id)
                .FirstOrDefaultAsync();

            if (stored == null)
            {
                return BadRequest("No stored credential found.");
            }

            try
            {
                var result = await fido2qi.MakeAssertionAsync(new MakeAssertionParams
                {
                    AssertionResponse = assertion,
                    OriginalOptions = originalOptions,
                    StoredPublicKey = Convert.FromBase64String(stored.PublicKey),
                    StoredSignatureCounter = stored.SignatureCounter,
                    IsUserHandleOwnerOfCredentialIdCallback = (userHandle, credentialId) => Task.FromResult(true)
                });

                if (result is not { CredentialId.Length: > 0 })
                {
                    return Unauthorized("Assertion failed.");
                }

                stored.SignatureCounter = result.SignCount;
                await _db.SaveChangesAsync();
                HttpContext.Session.SetString("IsLoggedIn", "true");

                return Ok(new { message = "Assertion verified. Session active." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception in /assertion/complete: " + ex.Message);
                Console.WriteLine("📜 StackTrace: " + ex.StackTrace);
                return BadRequest("Assertion verification error.");
            }

        }


    }
}
