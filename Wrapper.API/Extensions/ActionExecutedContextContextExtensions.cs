using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Wrapper.API.DTO;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Utils;

namespace Wrapper.API.Extensions
{
    /// <summary></summary>
    public static class ActionExecutedContextContextExtensions
    {
        /// <summary>
        /// When exception occurs, validation exceptions and not found exceptions are returned as 400, all other exceptions and unhandled exceptions are 500
        /// </summary>
        /// <remarks>
        /// In minor cases an unauthorized exception can be returned which can then be converted to a 401
        /// </remarks>
        /// <param name="context">Action executed context</param>
        /// <param name="operationContext">Request operation context</param>
        /// <param name="logger">Logger</param>
        public static void HandleException(this ActionExecutedContext context, IOperationContext operationContext, ILogger logger)
        {
            if (context.Exception != null)
            {
                if (context.Exception is ApiUnauthorizedException)
                {
                    // 401
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                else if (context.Exception is AccpacException)
                {
                    // 400 -> accpac related exception
                    AccpacException exception = context.Exception as AccpacException;
                    APIResponse<string> apiResponse = new APIResponse<string>($"Accpac error!", exception.Errors?.ToArray() ?? new string[] { });
                    context.Result = new BadRequestObjectResult(apiResponse);
                }
                else if (context.Exception is ValidationException)
                {
                    // 400 -> validation
                    ValidationException exception = context.Exception as ValidationException;
                    APIResponse<string> apiResponse = new APIResponse<string>($"Validation failed!", exception.Errors?.ToArray() ?? new string[] { });
                    context.Result = new BadRequestObjectResult(apiResponse);
                }
                else if (context.Exception is NotFoundExceptionBase)
                {
                    // 400 -> not found
                    ValidationException exception = context.Exception as ValidationException;
                    APIResponse<string> apiResponse = new APIResponse<string>($"Not found!", exception.Errors?.ToArray() ?? new string[] { "Entity not found" });
                    context.Result = new BadRequestObjectResult(apiResponse);
                }
                else
                {
                    // 500 -> handled and unhandled exceptions
                    string error = "An error occured processing operation";
                    if (context.Exception is HandledExceptionBase)
                    {
                        HandledExceptionBase exception = context.Exception as HandledExceptionBase;
                        ErrorUtils.LogException(operationContext, logger, exception, $"An error occured in operation, message: {exception.Message}");
                    }
                    else
                    {
                        error = "Unhandled exception in operation";
                        ErrorUtils.LogException(operationContext, logger, context.Exception, $"Unhandled exception in operation, message: {context.Exception.Message}");
                    }

                    APIResponse<string> apiResponse = new APIResponse<string>($"{error}");

                    context.Result = new JsonResult(apiResponse) { StatusCode = StatusCodes.Status500InternalServerError };
                }

                context.Exception = null;
            }
        }
    }
}
