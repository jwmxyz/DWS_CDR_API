using Cdr.ErrorManagement;

namespace Cdr.Api.Models
{
    /// <summary>
    /// An object that standardises the response objects from the API.
    /// </summary>
    public class CdrStandardResult
    {
        public object? Data { get; set; }
        public bool IsError { get; set; }
        public IEnumerable<CdrError> Errors { get; set; }

        public CdrStandardResult(object response)
        {
            Data = response;
        }

        public CdrStandardResult(object response, bool isError, IEnumerable<CdrError> errors) : this(response)
        {
            IsError = isError;
            Errors = errors;
        }

        public CdrStandardResult(object response, bool isError, CdrError error) : this(response, isError, new List<CdrError> { error })
        {
        }
    }
}
