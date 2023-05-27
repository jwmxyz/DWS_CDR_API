namespace Cdr.ErrorManagement
{
    public interface ICdrErrorManager
    {
        CdrError this[string code] { get; }

        /// <summary>
        /// Method that will log an error then then throw an excpetion 
        /// </summary>
        /// <typeparam name="T">The type of exception that needs to be thrown</typeparam>
        /// <param name="message">The message that needs to be logged.</param>
        /// <param name="inner">The inner exception that was thrown</param>
        T LogErrorAndReturnException<T>(string message, Exception inner) where T : Exception;

        /// <summary>
        /// Method that will log a warning then then throw an excpetion 
        /// </summary>
        /// <typeparam name="T">The type of exception that needs to be thrown</typeparam>
        /// <param name="message">The message that needs to be logged.</param>
        /// <param name="inner">The inner exception that was thrown</param>
        T LogWarningAndReturnException<T>(string message, Exception inner) where T : Exception;

        /// <summary>
        /// Method that will log a warning then then throw an excpetion 
        /// </summary>
        /// <typeparam name="T">The type of exception that needs to be thrown</typeparam>
        /// <param name="message">The message that needs to be logged.</param>
        T LogWarningAndReturnException<T>(string message) where T : Exception;

    }
}