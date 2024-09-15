using AccpacCOMAPI;
using Microsoft.AspNetCore.Http.Extensions;
using Wrapper.Models.Accpac;
using Wrapper.Services;
using Wrapper.Services.Accpac;
using Wrapper.Services.Api;
using Wrapper.Services.Utils;

namespace Wrapper.API.Services
{
    public class OperationContextFactory : IOperationContextFactory
    {
        private readonly ISessionProvider sessionProvider;
        private readonly IAccpacSessionModuleResolver moduleResolver;

        public OperationContextFactory(
                ISessionProvider sessionProvider,
                IAccpacSessionModuleResolver moduleResolver
            )
        {
            this.sessionProvider = sessionProvider;
            this.moduleResolver = moduleResolver;
        }

        public IOperationContext CreateAccpacOperationContext(HttpRequest request)
        {
            // Based on route identify default program and view and pass to session
            string url = request.GetDisplayUrl();
            AccpacSessionModuleDefault moduleDefaults = moduleResolver.GetModuleDefaultsBasedOnRoute(url);

            string dateTimeHeaderValue = request.Headers["X-DateTime"];
            DateTime sessionDate = string.IsNullOrEmpty(dateTimeHeaderValue) ? DateTime.UtcNow : DateTime.Parse(dateTimeHeaderValue);

            // open accpac session and database link
            AccpacSession accpacSession = sessionProvider.GetSession(moduleDefaults?.ProgramName, moduleDefaults?.StartView, sessionDate);
            AccpacDBLink mDBLinkCmpRW = accpacSession.OpenDBLink(tagDBLinkTypeEnum.DBLINK_COMPANY, tagDBLinkFlagsEnum.DBLINK_FLG_READWRITE);

            IOperationContext result = new AccpacOperationContext(request, accpacSession, mDBLinkCmpRW, moduleDefaults);

            return result;
        }

        public IOperationContext CreateSqlOperationContext(HttpRequest request)
        {
            IOperationContext result = new SqlOperationContext(request);
            return result;
        }

        private class AccpacOperationContext : IOperationContext
        {
            public AccpacSession AccpacSession { get; }
            private readonly string contextId = $"{Guid.NewGuid()}";
            private Guid userId;

            public string OperationId => contextId;

            public AccpacDBLink AccpacDBLink { get; }

            public Guid UserId => userId;

            public AccpacSessionModuleDefault AccpacSessionModuleDefault { get; }

            public AccpacOperationContext(HttpRequest request, AccpacSession session, AccpacDBLink accpacDBLink, AccpacSessionModuleDefault sessionModuleDefault)
            {
                AccpacSession = session;
                AccpacDBLink = accpacDBLink;
                AccpacSessionModuleDefault = sessionModuleDefault;

                userId = request.GetUserIdFromRequest();
            }

            public void Dispose()
            {
                if (AccpacSession != null)
                {
                    AccpacSession.Close();
                }

                if (AccpacDBLink != null)
                {
                    AccpacDBLink.Close();
                }
            }
        }

        private class SqlOperationContext : IOperationContext
        {
            private readonly string contextId = $"{Guid.NewGuid()}";
            private Guid userId;

            public SqlOperationContext(HttpRequest request)
            {
                userId = request.GetUserIdFromRequest();
            }

            public string OperationId => contextId;

            public Guid UserId => userId;

            public AccpacSession AccpacSession => throw new NotImplementedException();

            public AccpacDBLink AccpacDBLink => throw new NotImplementedException();

            public AccpacSessionModuleDefault AccpacSessionModuleDefault => throw new NotImplementedException();

            public void Dispose()
            {
            }
        }
    }
}
