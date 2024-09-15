using AccpacCOMAPI;
using Wrapper.Models.Accpac;

namespace Wrapper.Services
{
    /// <summary>
    /// Represents the context for a request operation, providing information such as operation ID, 
    /// Accpac session, database link, module defaults, and user ID.
    /// </summary>
    public interface IOperationContext
    {
        /// <summary>
        /// Each operation has a unique operation ID, which can be obtained by getting this property. 
        /// Use this in logging when the message is specific to the operation.
        /// </summary>
        string OperationId { get; }

        /// <summary>
        /// Gets the Accpac session object associated with the operation.
        /// </summary>
        AccpacSession AccpacSession { get; }

        /// <summary>
        /// Gets the Accpac database link object, used for interacting with the Accpac database.
        /// </summary>
        AccpacDBLink AccpacDBLink { get; }

        /// <summary>
        /// Gets the default settings for the Accpac session module, including program name and start view.
        /// </summary>
        AccpacSessionModuleDefault AccpacSessionModuleDefault { get; }

        /// <summary>
        /// Gets the ID of the user that sent the HTTP request.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Performs clean up of the operation context, releasing any resources held by the context.
        /// </summary>
        void Dispose();
    }

}
