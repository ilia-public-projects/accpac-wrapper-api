using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    public class ApInvoiceBatchLoader : IApInvoiceBatchLoader
    {
        private readonly ILogger<ApInvoiceBatchLoader> logger;
        private readonly IOptions<AppSettings> options;

        public ApInvoiceBatchLoader(
                ILogger<ApInvoiceBatchLoader> logger,
                IOptions<AppSettings> options
                )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<List<ApInvoiceBatchIdentifier>> GetActiveBatchIdentifiersAsync(IOperationContext context)
        {
            List<ApInvoiceBatchIdentifier> result = new List<ApInvoiceBatchIdentifier>();
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select CNTBTCH,BTCHDESC from APIBC with (nolock) where BTCHSTTS = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;


                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ApInvoiceBatchIdentifier item = new ApInvoiceBatchIdentifier
                            {
                                BatchNo = (int)reader.GetDecimal(0),
                                BatchDescription = reader.GetString(1),
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
