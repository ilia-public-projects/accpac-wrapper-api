using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.GLModels.GLSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.Accpac.GLModule.GLSetupServices
{
    public class GLAccountLoader: IGLAccountLoader
    {
        private readonly ILogger<GLAccountLoader> logger;
        private readonly IOptions<AppSettings> options;

        public GLAccountLoader(
                ILogger<GLAccountLoader> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<List<GLAccountModel>> GetActiveAccountsAsync(IOperationContext context)
        {
            List<GLAccountModel> result = new List<GLAccountModel>();
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select ACCTID,ACCTDESC from GLAMF with (nolock) where ACTIVESW = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;


                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            GLAccountModel item = new GLAccountModel
                            {
                                Code = reader.GetString(0).Trim(),
                                Description = reader.GetString(1).Trim()
                            };
                            result.Add(item);
                        }
                    }
                }
            }
            return result;
        }
    }
}
