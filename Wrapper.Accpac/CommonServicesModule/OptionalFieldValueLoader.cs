using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.APModels.APSetupModels;
using Wrapper.Models.Accpac.CommonServicesModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CommonServicesModule;

namespace Wrapper.Accpac.CommonServicesModule
{
    public class OptionalFieldValueLoader : IOptionalFieldValueLoader
    {
        private readonly IOptions<AppSettings> options;

        public OptionalFieldValueLoader(
                IOptions<AppSettings> options
            )
        {
            this.options = options;
        }

        public async Task<OptionalFieldValueModel> GetOptionaFieldValueByValueAsync(IOperationContext context, string code)
        {
            OptionalFieldValueModel result = null;
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select OPTFIELD, [value] from CSOPTFD with (nolock) where [value]=@Code";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;

                // add params
                command.Parameters.AddWithValue("@Code", code);

                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result = new OptionalFieldValueModel
                            {
                                OptionalField = reader.GetString(0).Trim(),
                                Value = reader.GetString(1).Trim()
                            };
                        }
                    }
                }
            }
            return result;
        }

        public async Task<bool> OptionalFieldDefinedInApLocationAsync(IOperationContext context, string optionalField, ApOptionalFieldLocation location)
        {
            bool isDefined = false;
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select count(*) from [APOFD] with (nolock) where [OPTFIELD]=@OptionalField and [LOCATION]=@Location";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 120;

                // add params
                command.Parameters.AddWithValue("@OptionalField", optionalField);
                command.Parameters.AddWithValue("@Location", (int)location);

                using (command)
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            isDefined = reader.GetInt32(0) > 0;
                        }
                    }
                }
            }   

            return isDefined;
        }

    }
}
