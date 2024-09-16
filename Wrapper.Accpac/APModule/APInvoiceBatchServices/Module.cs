using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    public static class Module
    {
        public static void AddAPInvoiceBatchServices(this IServiceCollection services)
        {
            services.AddSingleton<IAPInvoiceBatchEditor, APInvoiceBatchEditor>();
            services.AddSingleton<IAPInvoiceBatchValidator, APInvoiceBatchValidator>();
            services.AddSingleton<IApInvoiceBatchViewProcessor, ApInvoiceBatchViewProcessor>();

            services.AddSingleton<ApInvoiceBatchInsertProcessor>();
        }
    }
}
