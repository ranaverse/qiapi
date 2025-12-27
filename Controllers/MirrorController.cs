using Microsoft.AspNetCore.Mvc;
using qiapi.Mirrors;
using qiapi.Models;
using System.Diagnostics;
using System.Text.Json;

namespace qiapi.Controllers
{ 
    [ApiController]
    [Route("mirror")]
    public class MirrorController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Reflect([FromBody] MirrorRequest request)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = "C:\\Users\\sourc\\Documents\\QIField\\qimodels\\qi_mirror_model\\mirror_predict.py",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = psi };
            process.Start();

            using (var writer = process.StandardInput)
            {
                var inputJson = JsonSerializer.Serialize(new { text = request.Text });
                writer.WriteLine(inputJson);
                writer.Close(); // signal end of input
            }

            string output = await process.StandardOutput.ReadToEndAsync();
            Console.WriteLine("Mirror output: " + output);

            // return full response
            return Content(output, "application/json");

        }

    }
}
