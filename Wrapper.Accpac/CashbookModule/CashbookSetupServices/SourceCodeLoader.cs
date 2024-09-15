using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices;

namespace Wrapper.Accpac.CashbookModule.CashbookSetupServices
{
    public class SourceCodeLoader: ISourceCodeLoader
    {
        private readonly ILogger<SourceCodeLoader> logger;
        private readonly IOptions<AppSettings> options;

        public SourceCodeLoader(
                ILogger<SourceCodeLoader> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<List<SourceCodeModel>> GetSourceCodesAsync()
        {
            List<SourceCodeModel> result = new List<SourceCodeModel>();
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select SRCECODE,SRCEDESC from CBSRCE with (nolock)";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;


                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SourceCodeModel item = new SourceCodeModel
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
