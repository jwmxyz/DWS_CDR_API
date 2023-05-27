using Cdr.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cdr.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IUploadsServices _uploadServices;
        public UploadsController(IUploadsServices uploadServices) {
            _uploadServices = uploadServices;
        }

        [HttpPost, Route("csv")]
        public async Task<IActionResult> UploadCsv([FromForm] IFormFile csvFile)
        {
            await _uploadServices.Upload(csvFile);
            return Ok();
        }
    }
}
