using Microsoft.Extensions.DependencyInjection;
using Wrapper.Accpac.APModule;
using Wrapper.Accpac.CashbookModule;
using Wrapper.Accpac.CommonServicesModule;
using Wrapper.Accpac.GLModule;
using Wrapper.Services.Accpac;

namespace Wrapper.Accpac
{
    public static class Module
    {
        public static void AddAccpacServices(this IServiceCollection services)
        { 
            services.AddSingleton<ISessionProvider,SessionProvider>();
            services.AddSingleton<IAccpacSessionModuleResolver, AccpacSessionModuleResolver>();

            services.AddCommonServicesModule();
            services.AddCashbookModule();
            services.AddGLModule();
            services.AddAPModule();
        }
    }
}
