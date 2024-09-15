using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatch;

namespace Wrapper.Accpac.CashbookModule.NominalCashbookBatch
{
    public static class Module
    {
        public static void AddNominalCashbookServices(this IServiceCollection services)
        {
            services.AddSingleton<INominalCashbookBatchEditor, NominalCashbookBatchEditor>();
            services.AddSingleton<INominalCashbookBatchValidator, NominalCashbookBatchValidator>();
        }
    }
}
