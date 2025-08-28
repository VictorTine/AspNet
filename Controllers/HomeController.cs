using BlogX.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BlogX.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        [ApiKey]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}