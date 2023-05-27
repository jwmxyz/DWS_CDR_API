using Cdr.Api.Models;
using Cdr.ErrorManagement;
using Cdr.ErrorManagement.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Cdr.ErrorManagement.ErrorManager;

namespace Cdr.Api.Pipeline.Filters
{
    public class CdrExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            var errorManager = exceptionContext.HttpContext.RequestServices.GetService<ICdrErrorManager>();
            var logger = exceptionContext.HttpContext.RequestServices.GetService<ILogger<CdrExceptionFilter>>();

            if (exceptionContext.Exception.GetType() == typeof(InvalidCsvException))
            {
                logger?.LogError($"The provided CSV was invalid or contained errors");
                exceptionContext.Result = new ObjectResult(new CdrStandardResult(null, true, errorManager[ErrorCodes.INVALID_CSV_FILE]))
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
                return;
            }

            logger?.LogError($"An Error occured that was unhandled: {exceptionContext.Exception}");
            exceptionContext.Result = new ObjectResult(new CdrStandardResult(null, true, errorManager[ErrorCodes.INTERNAL_SERVER_ERROR]))
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
