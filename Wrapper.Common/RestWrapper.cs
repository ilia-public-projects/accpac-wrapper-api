using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Common;
using Wrapper.Services.Utils;

namespace Wrapper.Common
{
    public class RestWrapper : IRestWrapper
    {
        private readonly ILogger<RestWrapper> logger;

        public RestWrapper(
                ILogger<RestWrapper> logger
            )
        {
            this.logger = logger;
        }

        public async Task<T> PostAsync<T>(IOperationContext context, string url, object data, string authHeaderPrefix, string secret)
        {
            T result = default;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(secret))
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"{authHeaderPrefix}{secret}");
                    }

                    string json = JsonConvert.SerializeObject(data);

                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                    string content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        result = !string.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<T>(content) : default;
                    }
                    else
                    {
                        string logMessage = $"Error code returned from REST request to url ({url}), message: {content}";
                        ErrorUtils.LogAndThrowException(context, logger, logMessage, () => throw new RestWrapperException(logMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                string logMessage = $"An error occured while sending REST request to url ({url}), message: {ex.Message}";
                ErrorUtils.LogAndThrowException(context, logger, logMessage, () => throw new RestWrapperCommunicationException(logMessage));
            }
            return result;
        }
    }
}
