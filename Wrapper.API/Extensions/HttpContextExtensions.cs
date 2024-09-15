using Wrapper.Services;

namespace Wrapper.API.Extensions
{
    /// <summary>
    /// Provides extension methods for the HttpContext class.
    /// </summary>
    public static class HttpContextExtensions
    {
        const string ActionItemOperationContext = "ActionItemOperationContext";

        /// <summary>
        /// Fetches the operation context from the HttpContext.
        /// </summary>
        public static IOperationContext GetOperationContext(this HttpContext context)
        {
            return context.Items[ActionItemOperationContext] as IOperationContext;
        }

        /// <summary>
        /// Stores the operation context in the HttpContext.
        /// </summary>
        public static void SetOperationContext(this HttpContext context, IOperationContext operationContext)
        {
            context.Items[ActionItemOperationContext] = operationContext;
        }
    }
}
