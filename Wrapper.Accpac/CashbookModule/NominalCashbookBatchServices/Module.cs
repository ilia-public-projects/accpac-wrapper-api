using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatchServices;

namespace Wrapper.Accpac.CashbookModule.NominalCashbookBatchServices
{
    public static class Module
    {
        public static void AddNominalCashbookServices(this IServiceCollection services)
        {
            services.AddSingleton<INominalCashbookBatchEditor, NominalCashbookBatchEditor>();
            services.AddSingleton<INominalCashbookBatchValidator, NominalCashbookBatchValidator>();
            services.AddSingleton<NominalCashbookBatchInsertProcessor>();
        }
    }
}
