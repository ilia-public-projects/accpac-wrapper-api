using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wrapper.Models.Accpac.APModels.APSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APSetupServices;

namespace Wrapper.Accpac.APModule.APSetupServices
{
    public class VendorLoader: IVendorLoader
    {
        private readonly ILogger<VendorLoader> logger;
        private readonly IOptions<AppSettings> options;

        public VendorLoader(
                ILogger<VendorLoader> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<VendorModel> GetVendorByCodeAsync(IOperationContext context, string code)
        {
            VendorModel result = null;
            using (SqlConnection connection = new SqlConnection(options.Value.AccpacDbConnectionString))
            {
                connection.Open();

                string query = $"select VENDORID,VENDNAME,IDGRP from APVEN with (nolock) where SWACTV = 1 and VENDORID = @Code";
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
                            result = new VendorModel
                            {
                                Code = reader.GetString(0).Trim(),
                                Name = reader.GetString(1).Trim(),
                                GroupCode = reader.GetString(2).Trim()
                            };
                        }
                    }
                }
            }
            return result;
        }
    }
}
