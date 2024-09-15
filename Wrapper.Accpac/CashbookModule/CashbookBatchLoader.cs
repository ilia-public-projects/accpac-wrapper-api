using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;

namespace Wrapper.Accpac.CashbookModule
{
    public class CashbookBatchLoader : ICashbookBatchLoader
    {
        private readonly ILogger<CashbookBatchLoader> logger;
        private readonly IOptions<AppSettings> options;

        public CashbookBatchLoader(
            ILogger<CashbookBatchLoader> logger,
            IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<List<CashbookBatchIdentifier>> GetActiveBatchIdentifiersAsync(IOperationContext context)
        {
            List<CashbookBatchIdentifier> result = new List<CashbookBatchIdentifier>();
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select BATCHID,TEXTDESC from CBBCTL with (nolock) where status = 0 and BATCHTYPE = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;


                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CashbookBatchIdentifier item = new CashbookBatchIdentifier
                            {
                                BatchNo = reader.GetString(0).Trim(),
                                BatchDescription = reader.GetString(1).Trim()
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
