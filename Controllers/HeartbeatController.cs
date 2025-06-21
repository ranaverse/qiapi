using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qiapi.Models;

namespace qiapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeartbeatController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new Heartbeat
            {
                Message = "Heartbeat received. The oracle is alive.",
                Timestamp = DateTime.UtcNow,
                Source = "QI Core"
            };

            return Ok(response);
        }
    }

}
