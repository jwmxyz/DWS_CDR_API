using Cdr.Api.Models;
using Cdr.Api.Pipeline.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cdr.Api.Controllers
{
    [IncomingRequestModelValidationActionFilter]
    public class SharedController<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;

        public SharedController() { }

        public SharedController(ILogger<T> logger)
        {
            _logger = logger;
        }
        public override OkObjectResult Ok(object? content)
        {
            var response = new CdrStandardResult(content);
            return new OkObjectResult(response);
        }
    }
}
