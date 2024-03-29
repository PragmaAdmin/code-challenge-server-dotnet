using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var http = new HttpClient();
            var url = string.Format("https://hasydbj5c4gpa2oozfpjpc677a0hxuob.lambda-url.ap-southeast-2.on.aws/sensor/{0}", id);

            var response = await http.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var sensorData = JsonSerializer.Deserialize<Sensor>(jsonString, new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(sensorData);
        }
    }
}
