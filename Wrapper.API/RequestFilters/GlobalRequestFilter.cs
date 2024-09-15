using Microsoft.AspNetCore.Mvc.Filters;
using Wrapper.API.Extensions;
using Wrapper.Services;
using Wrapper.Services.Api;

namespace Wrapper.RequestFilters
{
    /// <summary></summary>
    public class GlobalRequestFilter : IActionFilter
    {
        private readonly ILogger<GlobalRequestFilter> logger;
        private readonly IOperationContextHandler operationContextHandler;

        /// <summary></summary>
        public GlobalRequestFilter(
            ILogger<GlobalRequestFilter> logger,
            IOperationContextHandler operationContextHandler
            )
        {
            this.logger = logger;
            this.operationContextHandler = operationContextHandler;
        }

        /// <summary></summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            IOperationContext operationContext = context.HttpContext.GetOperationContext();
            if (null != operationContext)
            {
                operationContext.Dispose();
                logger.LogInformation($"END,{operationContext.OperationId},{context.HttpContext.Request.Method},{context.HttpContext.Request.Path},{context.HttpContext.Request.QueryString}");
            }

            context.HandleException(operationContext, logger);
        }

        /// <summary></summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IOperationContext operationContext = operationContextHandler.HandleOperationContext(context.HttpContext);

            logger.LogInformation($"START,{operationContext.OperationId},{context.HttpContext.Request.Method},{context.HttpContext.Request.Path},{context.HttpContext.Request.QueryString}");
        }


    }
}
