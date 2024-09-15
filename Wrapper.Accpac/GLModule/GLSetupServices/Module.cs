using Microsoft.Extensions.DependencyInjection;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.Accpac.GLModule.GLSetupServices
{
    public static class Module
    {
        public static void AddGLSetupServices(this IServiceCollection services)
        {
            services.AddSingleton<IGLAccountLoader, GLAccountLoader>();
            services.AddSingleton<IGLSetupValidator, GLSetupValidator>();
        }
    }
}
