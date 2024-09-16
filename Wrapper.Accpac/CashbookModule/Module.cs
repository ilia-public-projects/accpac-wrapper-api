using Microsoft.Extensions.DependencyInjection;
using Wrapper.Accpac.CashbookModule.ApCashbookBatchServices;
using Wrapper.Accpac.CashbookModule.CashbookSetupServices;
using Wrapper.Accpac.CashbookModule.NominalCashbookBatchServices;
using Wrapper.Services.Accpac.CashbookModule;

namespace Wrapper.Accpac.CashbookModule
{
    public static class Module
    {
        public static void AddCashbookModule(this IServiceCollection services)
        {
            services.AddCashbookSetupServices();
            services.AddNominalCashbookServices();
            services.AddApCashbookServices();

            services.AddSingleton<ICashbookBatchViewComposer, CashbookBatchViewComposer>();
            services.AddSingleton<ICashbookBatchLoader, CashbookBatchLoader>();
        }
    }
}
