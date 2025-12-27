using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Fido2NetLib;
using Fido2NetLib.Objects;
using System.Text.Json;

namespace qiapi.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class FidoAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;

            try
            {
                var assertionResponse = await JsonSerializer.DeserializeAsync<AuthenticatorAssertionRawResponse>(
                    request.Body,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (assertionResponse == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var fido2 = GetFido2Instance();

                var makeAssertionParams = new MakeAssertionParams
                {
                    AssertionResponse = assertionResponse,
                    OriginalOptions = GetAssertionOptionsFromSessionOrCache(assertionResponse.Id),
                    StoredPublicKey = GetPublicKeyFromDatabase(assertionResponse.Id),
                    StoredSignatureCounter = GetSignatureCounterForCredential(assertionResponse.Id),
                    IsUserHandleOwnerOfCredentialIdCallback = (userHandle, credentialId) => Task.FromResult(true)
                };


                var result = await fido2.MakeAssertionAsync(makeAssertionParams);

                if (result is not { CredentialId.Length: > 0 })
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                await next(); // All clear, continue to controller
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }

        }
        // ✦ Implement your own data layer logic here

        private AssertionOptions GetAssertionOptionsFromSessionOrCache(string credentialId)
        {
            throw new NotImplementedException();
        }

        private byte[] GetPublicKeyFromDatabase(string credentialId)
        {
            throw new NotImplementedException();
        }

        private uint GetSignatureCounterForCredential(string credentialId)
        {
            throw new NotImplementedException();
        }

        private Fido2 GetFido2Instance()
        {
            return new Fido2(new Fido2Configuration
            {
                ServerDomain = "localhost",
                ServerName = "QI",
                Origins = new HashSet<string>
        {
            "http://localhost:4200"  // Your Angular dev origin
        }
            });
        }

    }
}
