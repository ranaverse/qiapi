using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qiapi.Models;
using qiapi.Security;

namespace qiapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeartbeatController : ControllerBase
    {
        [FidoAuthorize]
        [HttpGet]
        public IActionResult Get()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return Unauthorized();
            }

            return Ok("🔥 QI is alive and guarded.");
        }

    }

}
