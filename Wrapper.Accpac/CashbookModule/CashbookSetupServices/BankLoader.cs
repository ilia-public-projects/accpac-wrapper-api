using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices;

namespace Wrapper.Accpac.CashbookModule.CashbookSetupServices
{
    public class BankLoader : IBankLoader
    {
        private readonly ILogger<BankLoader> logger;
        private readonly IOptions<AppSettings> options;

        public BankLoader(
                ILogger<BankLoader> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<List<BankModel>> GetActiveBanksAsync(IOperationContext context)
        {
            List<BankModel> result = new List<BankModel>();
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"SELECT Bank as BankCode, [Name] as [Name], CURNSTMT as Currency, INACTIVE as Inactive  FROM [BKACCT] with (nolock)";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;


                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            BankModel item = new BankModel
                            {
                                BankCode = reader.GetString(0).Trim(),
                                Name = reader.GetString(1).Trim(),
                                Currency = reader.GetString(2).Trim(),
                                IsActive = reader.GetInt16(3) == 0 ? true : false
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
