using AccpacCOMAPI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Common;
using Wrapper.Services.Accpac;

namespace Wrapper.Accpac
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ILogger<SessionProvider> logger;
        private readonly AppSettings appSettings;

        public SessionProvider(
                ILogger<SessionProvider> logger,
                IOptions<AppSettings> options
            )
        {
            appSettings = options.Value;
            this.logger = logger;
        }

        public AccpacSession GetSession(string programName, string startView, DateTime sessionDate)
        {
            try
            {
                AccpacSession accpacSession = new AccpacSession();
                accpacSession.Init(ObjectHandle: "", programName, startView, appSettings.AccpacVersion);
                accpacSession.Open(appSettings.AccpacUsername, appSettings.AccpacPassword, appSettings.AccpacCompany, sessionDate, Flags: 0, Reserved: "");

                return accpacSession;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to login to accpac, message:{ex.Message}");
                throw;
            }
        }
    }
}
