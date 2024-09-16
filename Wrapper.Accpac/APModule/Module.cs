using Microsoft.Extensions.DependencyInjection;
using Wrapper.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Accpac.APModule.APSetupServices;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;

namespace Wrapper.Accpac.APModule
{
    public static class Module
    {
        public static void AddAPModule(this IServiceCollection services)
        {
            services.AddSingleton<IApInvoiceBatchViewComposer, ApInvoiceBatchViewComposer>();
            services.AddSingleton<IApInvoiceBatchLoader, ApInvoiceBatchLoader>();

            services.AddAPSetupServices();
            services.AddAPInvoiceBatchServices();

            
        }
    }
}
