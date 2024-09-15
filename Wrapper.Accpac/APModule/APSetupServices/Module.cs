using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Accpac.APModule.APSetupServices;

namespace Wrapper.Accpac.APModule.APSetupServices
{
    public static class Module
    {
        public static void AddAPSetupServices(this IServiceCollection services)
        {
            services.AddSingleton<IVendorLoader, VendorLoader>();
            services.AddSingleton<IApModuleSetupValidator, ApModuleSetupValidator>();

        }
    }
}
