using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Common;
using Wrapper.Services.Utils;

namespace Wrapper.Common
{
    public class NotificationMessenger : INotificationMessenger
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly IRestWrapper restWrapper;
        private readonly ILogger<NotificationMessenger> logger;

        public NotificationMessenger(
                IOptions<AppSettings> appSettings,
                IRestWrapper restWrapper,
                ILogger<NotificationMessenger> logger
            )
        {
            this.appSettings = appSettings;
            this.restWrapper = restWrapper;
            this.logger = logger;
        }

        public async Task NotifyProgressAsync(IOperationContext context, NotificationModel notification)
        {
            try
            {
                string apiBaseUrl = appSettings.Value.NotificationSystemApiUri;
                apiBaseUrl = apiBaseUrl.TrimEnd('/');

                string url = $"{apiBaseUrl}{NotificationSystemUrlConstants.SubmitNotificationEndpointV1}";

                string communicationSecret = appSettings.Value.NotificationSystemApiAuthSecret;

                await restWrapper.PostAsync<dynamic>(context, url, notification, authHeaderPrefix: "Secret ", communicationSecret);
            }
            catch (RestWrapperCommunicationException ex)
            {
                ErrorUtils.LogException(context, logger, ex, "Failed to send progress notification message");
            }
        }
    }
}
