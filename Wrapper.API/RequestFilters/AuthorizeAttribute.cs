using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Wrapper.Models.Common;

namespace Wrapper.RequestFilters
{
    /// <summary>
    /// Makes sure that the request is authorized by validating the Authorization header against the secret key in appsettings.json
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary></summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            IOptions<AppSettings> options = context.HttpContext.RequestServices.GetService<IOptions<AppSettings>>();
            AppSettings appSettings = options.Value;

            // authorization
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
            {
                // Authorization header exists, check its value
                if (appSettings.ApiAuthSecret != authHeaderValue)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            else
            {
                // Authorization header does not exist
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
