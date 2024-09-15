using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Common;

namespace Wrapper.Common
{
    public static class Module
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<INotificationMessenger, NotificationMessenger>();
            services.AddSingleton<IRestWrapper, RestWrapper>();
        }
    }
}
