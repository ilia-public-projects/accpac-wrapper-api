using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.Services.Accpac.CommonServicesModule;

namespace Wrapper.Accpac.CommonServicesModule
{
    public static class Module
    {
        public static void AddCommonServicesModule(this IServiceCollection services)
        {
            services.AddSingleton<ICurrencyLoader, CurrencyLoader>();
            services.AddSingleton<ICommonServicesValidator, CommonServicesValidator>();
            services.AddSingleton<IOptionalFieldValueLoader, OptionalFieldValueLoader>();
        }
    }
}
