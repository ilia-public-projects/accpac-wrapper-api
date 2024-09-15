using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.CommonServicesModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CommonServicesModule;

namespace Wrapper.Accpac.CommonServicesModule
{
    public class CurrencyLoader : ICurrencyLoader
    {
        private readonly ILogger<CurrencyLoader> logger;
        private readonly IOptions<AppSettings> options;

        public CurrencyLoader(
                ILogger<CurrencyLoader> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<List<CurrencyModel>> GetCurrenciesAsync(IOperationContext context)
        {
            List<CurrencyModel> result = new List<CurrencyModel>();
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select * from CSCCD with (nolock)";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;


                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CurrencyModel item = new CurrencyModel
                            {
                                Code = reader.GetString(0),
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
