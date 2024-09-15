using Wrapper.Models.Common;

namespace Wrapper.Services.Common
{
    /// <summary>
    /// Provides functionality for sending notifications, including progress updates.
    /// </summary>
    public interface INotificationMessenger
    {
        /// <summary>
        /// Sends a progress notification asynchronously via a configured API endpoint.
        /// </summary>
        /// <param name="context">The operation context containing necessary request details.</param>
        /// <param name="notification">The notification model containing the details of the progress message to be sent.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task NotifyProgressAsync(IOperationContext context, NotificationModel notification);
    }

}
