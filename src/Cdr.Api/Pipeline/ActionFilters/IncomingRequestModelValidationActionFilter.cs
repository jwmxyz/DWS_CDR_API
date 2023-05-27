using Cdr.Api.Models;
using Cdr.ErrorManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Cdr.ErrorManagement.ErrorManager;

namespace Cdr.Api.Pipeline.ActionFilters
{
    public class IncomingRequestModelValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errorManager = actionContext.HttpContext.RequestServices.GetService<ICdrErrorManager>();

                var errors = modelState.Values.SelectMany(v => v.Errors);

                var validationErrors = errors
                    .Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
                    .Select(x => errorManager[x.ErrorMessage]).ToList();

                if (errors.Any(x => x.Exception?.GetType() == typeof(Newtonsoft.Json.JsonReaderException)))
                {
                    validationErrors.Add(errorManager[ErrorCodes.INVALID_JSON_DATA]);
                }

                actionContext.Result = new ObjectResult(new CdrStandardResult(null, true, validationErrors))
                {
                    StatusCode = StatusCodes.Status412PreconditionFailed,
                };
            }
        }
    }
}
