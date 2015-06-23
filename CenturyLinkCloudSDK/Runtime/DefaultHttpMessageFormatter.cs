using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    internal class DefaultHttpMessageFormatter : IHttpMessageFormatter
    {
        public HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string serviceUri, ServiceModels.Authentication authentication, object content)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, serviceUri);
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));

            if (authentication != null)
            {
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authentication.BearerToken);
            }

            if (content != null)
            {
                var serializedContent = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(serializedContent, new UTF8Encoding(), Constants.ServiceUris.JsonMediaType);
            }            

            return httpRequestMessage;
        }

        public Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response)
        {
            return response.Content.ReadAsAsync<TResponse>();
        }

        public async Task<string> DeserializeErrorResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return string.Empty;

            var apiError = default(ServiceModels.ApiError);
            try
            {
                apiError = await response.Content.ReadAsAsync<ServiceModels.ApiError>().ConfigureAwait(false);
            }catch
            {
                //ignore - it'd be nice if we had a TryReadAsAsync
            }

            if (apiError == default(ServiceModels.ApiError)) return string.Empty;

            var errorMessages = new StringBuilder();
            if (!string.IsNullOrEmpty(apiError.Message))
            {
                errorMessages.AppendLine(apiError.Message);
            }

            if(apiError.ModelState != null)
            {
                foreach(var error in apiError.ModelState.SelectMany(prop => prop.Value))
                {
                    errorMessages.AppendLine(error);
                }
            }

            return errorMessages.ToString();
        }
    }
}
