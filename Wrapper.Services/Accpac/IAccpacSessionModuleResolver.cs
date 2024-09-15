using Wrapper.Models.Accpac;

namespace Wrapper.Services.Accpac
{
    /// <summary>
    /// Resolves the default settings for Accpac sessions based on the provided URL route.
    /// </summary>
    public interface IAccpacSessionModuleResolver
    {
        /// <summary>
        /// Gets the module defaults based on the provided URL.
        /// </summary>
        /// <param name="url">The URL to check for route segments.</param>
        /// <returns>The module defaults corresponding to the route segment in the URL, or the default value if no match is found.</returns>
        AccpacSessionModuleDefault GetModuleDefaultsBasedOnRoute(string url);
    }
}
