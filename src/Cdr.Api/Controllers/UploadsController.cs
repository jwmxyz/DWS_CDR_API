using Cdr.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cdr.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class UploadsController : SharedController<UploadsController>
    {
        private readonly IUploadsServices _uploadServices;
        public UploadsController(IUploadsServices uploadServices) {
            _uploadServices = uploadServices;
        }

        [HttpPost, Route("callRecords")]
        public async Task<IActionResult> UploadCsv([FromForm] IFormFile csvFile)
        {
            var result = await _uploadServices.UploadCallRecords(csvFile);
            return Ok(result);
        }

        [HttpDelete, Route("Flush")]
        public async Task<IActionResult> Flush()
        {
            await _uploadServices.Flush();
            return Ok();
        }

    }
}
