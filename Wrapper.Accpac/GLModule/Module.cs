using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.Accpac.GLModule.GLSetupServices;

namespace Wrapper.Accpac.GLModule
{
    public static class Module
    {
        public static void AddGLModule(this IServiceCollection services)
        {
            services.AddGLSetupServices();
        }
    }
}
