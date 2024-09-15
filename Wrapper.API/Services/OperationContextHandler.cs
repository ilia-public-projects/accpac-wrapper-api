using Wrapper.API.Extensions;
using Wrapper.Services;
using Wrapper.Services.Api;

namespace Wrapper.API.Services
{
    public class OperationContextHandler: IOperationContextHandler
    {
        private readonly IOperationContextFactory operationContextFactory;
        private readonly ILogger<OperationContextHandler> logger;

        public OperationContextHandler(
                IOperationContextFactory operationContextFactory,
                ILogger<OperationContextHandler> logger
            )
        {
            this.operationContextFactory = operationContextFactory;
            this.logger = logger;
        }

        public IOperationContext HandleOperationContext(HttpContext httpContext)
        {
            // Avoid creating accpac operation context if the operation type is SQL, since it doesn't require a lanpack (Accpac connection)
            bool isSqlOperation = DetermineIfSqlOperation(httpContext);

            // Create the appropriate operation context
            IOperationContext operationContext = CreateOperationContext(httpContext, isSqlOperation);

            // Set the operation context in the HTTP context
            httpContext.SetOperationContext(operationContext);

            return operationContext;
        }

        private bool DetermineIfSqlOperation(HttpContext httpContext)
        {
            return httpContext.Request.Headers.ContainsKey("X-OperationType") &&
                   httpContext.Request.Headers["X-OperationType"].ToString().Equals("SQL", StringComparison.OrdinalIgnoreCase);
        }

        private IOperationContext CreateOperationContext(HttpContext httpContext, bool isSqlOperation)
        {
            return isSqlOperation
                ? operationContextFactory.CreateSqlOperationContext(httpContext.Request)
                : operationContextFactory.CreateAccpacOperationContext(httpContext.Request);
        }
    }
}
