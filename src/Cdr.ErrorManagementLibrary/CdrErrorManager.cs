using Microsoft.Extensions.Logging;
using static Cdr.ErrorManagement.ErrorManager;

namespace Cdr.ErrorManagement
{
    public partial class CdrErrorManager : ICdrErrorManager
    {
        private readonly ILogger<CdrErrorManager> _logger;

        public CdrErrorManager(ILogger<CdrErrorManager> logger)
        {
            _logger = logger;
        }

        private readonly List<CdrError> Errors = new()
        {
            new CdrError(ErrorCodes.INTERNAL_SERVER_ERROR, "Something went wrong with your request."),
            new CdrError(ErrorCodes.INVALID_JSON_DATA, "The JSON data that you have submitted is invalid."),
            new CdrError(ErrorCodes.INVALID_DATE, "The date you have submitted is invalid."),
            new CdrError(ErrorCodes.INVALID_CSV_FILE, "The provided CSV file is invalid or contains errors."),
        };

        public CdrError this[string code]
        {
            get
            {
                var error = Errors.FirstOrDefault(x => x.Code == code);
                return error ?? throw new NullReferenceException($"Error with code {code} could not be found");
            }
        }

        /// <inheritdoc cref="ICdrErrorManager.LogErrorAndReturnException{T}(string)" />
        public T LogErrorAndReturnException<T>(string message, Exception inner) where T : Exception
        {
            _logger.LogError(message, inner);
            return Activator.CreateInstance(typeof(T), message) as T ?? throw new Exception("Error creating generic exception, last message: " + message);
        }

        /// <inheritdoc cref="ICdrErrorManager.LogWarningAndReturnException{T}(string){T}(string)" />
        public T LogWarningAndReturnException<T>(string message, Exception inner) where T : Exception
        {
            _logger.LogWarning(message, inner);
            return Activator.CreateInstance(typeof(T), message, inner) as T ?? throw new Exception("Error creating generic exception, last message: " + message);
        }
    }
}