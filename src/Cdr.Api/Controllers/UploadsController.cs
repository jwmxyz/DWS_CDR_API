using Microsoft.AspNetCore.Mvc;

namespace Cdr.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class UploadsController : ControllerBase
    {
        public UploadsController() { }

        [HttpGet, Route("Upload")]
        public IActionResult Upload()
        {
            return Ok("Hello world");
        }
    }
}
