using Microsoft.AspNetCore.Http;

namespace Wrapper.Services.Api
{
    /// <summary>
    /// Defines the contract for creating operation contexts for different types of requests,
    /// including Accpac operation contexts and SQL operation contexts.
    /// </summary>
    public interface IOperationContextFactory
    {
        /// <summary>
        /// Creates an Accpac operation context based on the given HTTP request.
        /// The context is initialized using the session provider and Accpac module defaults,
        /// such as program name and start view, determined by the request route.
        /// </summary>
        /// <param name="request">The HTTP request containing the headers and route information.</param>
        /// <returns>An <see cref="IOperationContext"/> initialized for Accpac operations.</returns>
        IOperationContext CreateAccpacOperationContext(HttpRequest request);

        /// <summary>
        /// Creates a SQL operation context based on the given HTTP request.
        /// The context is initialized for SQL-based operations, with headers
        /// extracted from the request if needed.
        /// </summary>
        /// <param name="request">The HTTP request containing headers and context information.</param>
        /// <returns>An <see cref="IOperationContext"/> initialized for SQL operations.</returns>
        IOperationContext CreateSqlOperationContext(HttpRequest request);
    }

}
