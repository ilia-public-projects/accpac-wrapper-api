using Microsoft.AspNetCore.Http;

namespace Wrapper.Services.Utils
{
    /// <summary>
    /// Provides utility methods for handling HTTP requests.
    /// </summary>
    public static class HttRequestUtils
    {
        /// <summary>
        /// Retrieves the user ID from the "X-UserId" header of the HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request from which to extract the user ID.</param>
        /// <returns>
        /// The user ID if the "X-UserId" header is present and contains a valid GUID; otherwise, <see cref="Guid.Empty"/>.
        /// </returns>
        public static Guid GetUserIdFromRequest(this HttpRequest request)
        {
            if (request.Headers.ContainsKey("X-UserId"))
            {
                if (Guid.TryParse(request.Headers["X-UserId"], out Guid userId))
                {
                    return userId;
                }
            }

            return default;
        }
    }
}
