using Microsoft.AspNetCore.Http;

namespace Wrapper.Services.Api
{
    /// <summary>
    /// Handles the creation and management of operation contexts based on the incoming HTTP request.
    /// </summary>
    public interface IOperationContextHandler
    {
        /// <summary>
        /// Handles the creation of an operation context based on the HTTP request.
        /// Determines if the operation is SQL-based or Accpac-based and creates the appropriate context.
        /// Sets the created operation context in the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context containing the request information.</param>
        /// <returns>The created operation context.</returns>
        IOperationContext HandleOperationContext(HttpContext httpContext);
    }
}
