using Wrapper.Models.Common.Exceptions;

namespace Wrapper.Services.Common
{
    /// <summary>
    /// Provides functionality to make REST API requests with logging and error handling.
    /// </summary>
    public interface IRestWrapper
    {
        /// <summary>
        /// Sends an asynchronous HTTP POST request to the specified URL with the provided data and authorization headers.
        /// </summary>
        /// <typeparam name="T">The type to which the response content will be deserialized.</typeparam>
        /// <param name="context">The operation context containing information about the current operation, including request details.</param>
        /// <param name="url">The URL to which the POST request will be sent.</param>
        /// <param name="data">The data to be serialized and sent in the POST request body.</param>
        /// <param name="authHeaderPrefix">The prefix for the Authorization header (e.g., "Bearer ").</param>
        /// <param name="secret">The authorization secret or token to be included in the request headers.</param>
        /// <returns>The deserialized response content as an object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="RestWrapperException">Thrown if the REST request results in an error response.</exception>
        /// <exception cref="RestWrapperCommunicationException">Thrown if a communication error occurs while sending the REST request.</exception>
        Task<T> PostAsync<T>(IOperationContext context, string url, object data, string authHeaderPrefix, string secret);
    }
}
