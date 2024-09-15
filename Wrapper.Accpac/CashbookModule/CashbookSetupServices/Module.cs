using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices;

namespace Wrapper.Accpac.CashbookModule.CashbookSetupServices
{
    public static class Module
    {
        public static void AddCashbookSetupServices(this IServiceCollection services)
        {
            services.AddSingleton<IBankLoader, BankLoader>();
            services.AddSingleton<ISourceCodeLoader, SourceCodeLoader>();
            services.AddSingleton<ICashbookSetupValidor, CashbookSetupValidor>();
        }
    }
}
