using Microsoft.Extensions.Logging;
using Wrapper.Models.Common.Exceptions;

namespace Wrapper.Services.Utils
{

    /// <summary>
    /// Provides utility methods for logging errors and throwing exceptions.
    /// </summary>
    public static class ErrorUtils
    {
        /// <summary>
        /// Logs an error message and throws a custom exception.
        /// </summary>
        /// <param name="operationContext">The context of the current operation.</param>
        /// <param name="logger">The logger used to log the error message.</param>
        /// <param name="message">The error message to log.</param>
        /// <param name="throwException">The action that throws the exception.</param>
        public static void LogAndThrowException(IOperationContext operationContext, ILogger logger, string message, Action throwException)
        {
            logger.LogError($"{operationContext.OperationId}:{message}");
            throwException();
        }

        /// <summary>
        /// Logs an exception along with a custom error message.
        /// </summary>
        /// <param name="operationContext">The context of the current operation.</param>
        /// <param name="logger">The logger used to log the error message and exception.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="message">The custom error message to log with the exception.</param>
        public static void LogException(IOperationContext operationContext, ILogger logger, Exception ex, string message)
        {
            logger.LogError(ex, $"{operationContext.OperationId}:{message}");
        }

        /// <summary>
        /// Logs Accpac-specific errors and throws an <see cref="AccpacException"/> if Accpac errors are present, 
        /// otherwise logs the error and executes a custom unhandled exception action.
        /// </summary>
        /// <param name="context">The context of the current operation.</param>
        /// <param name="logger">The logger used to log the error message.</param>
        /// <param name="message">The error message to log.</param>
        /// <param name="unhandledExceptionAction">The action to execute if there are no Accpac errors.</param>
        /// <exception cref="AccpacException">Thrown when Accpac errors are present.</exception>
        public static void LogAndThrowAccpacException(IOperationContext context, ILogger logger, string message, Action unhandledExceptionAction)
        {
            if (context.AccpacSession.Errors != null && context.AccpacSession.Errors.Count > 0)
            {
                List<string> errors = new List<string>();
                for (int i = 0; i < context.AccpacSession.Errors.Count; i++)
                {
                    string error = context.AccpacSession.Errors.Item(i);
                    errors.Add(error);
                }

                logger.LogError($"{context.OperationId}:{message}");
                throw new AccpacException(message, errors);
            }
            else
            {
                logger.LogError($"{context.OperationId}:{message}");
                unhandledExceptionAction();
            }
        }
    }

}
