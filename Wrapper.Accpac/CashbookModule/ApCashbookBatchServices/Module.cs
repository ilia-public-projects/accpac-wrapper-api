using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices;

namespace Wrapper.Accpac.CashbookModule.ApCashbookBatchServices
{
    public static class Module
    {
        public static void AddApCashbookServices(this IServiceCollection services)
        {

            services.AddSingleton<IApCashbookBatchEditor, ApCashbookBatchEditor>();
            services.AddSingleton<IApCashbookBatchValidator, ApCashbookBatchValidator>();
        }
    }
}
