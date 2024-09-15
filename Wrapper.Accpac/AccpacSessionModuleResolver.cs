using Microsoft.Extensions.Logging;
using Wrapper.Models.Accpac;
using Wrapper.Services.Accpac;

namespace Wrapper.Accpac
{
    public class AccpacSessionModuleResolver : IAccpacSessionModuleResolver
    {


        private const string cashbookRouteSegment = "cashbook";
        private const string accountsPayableRouteSegment = "accounts-payable";
        private readonly ILogger<AccpacSessionModuleResolver> logger;

        // Define route-to-module mappings
        private Dictionary<string, AccpacSessionModuleDefault> routeToModuleMapping = new Dictionary<string, AccpacSessionModuleDefault>
        {
            { cashbookRouteSegment, new AccpacSessionModuleDefault { ProgramName = "CB", StartView = AccpacViewConstants.CBBatchControlView } },
            { accountsPayableRouteSegment, new AccpacSessionModuleDefault { ProgramName = "AP", StartView = AccpacViewConstants.APBatchControlView } }
        };

        public AccpacSessionModuleResolver(
                ILogger<AccpacSessionModuleResolver> logger
            )
        {
            this.logger = logger;
        }

        public AccpacSessionModuleDefault GetModuleDefaultsBasedOnRoute(string url)
        {

            // Determine module defaults based on URL
            foreach (var route in routeToModuleMapping.Keys)
            {
                if (url.Contains(route, StringComparison.OrdinalIgnoreCase))
                {
                    return routeToModuleMapping[route];
                }
            }

            logger.LogWarning($"No module defaults found for URL: {url}");

            return default;
        }
    }
}
