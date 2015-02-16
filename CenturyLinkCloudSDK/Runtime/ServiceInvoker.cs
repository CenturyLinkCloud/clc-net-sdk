using CenturyLinkCloudSDK.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    internal static class ServiceInvoker
    {
        private static HttpClient httpClient;

        static ServiceInvoker()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Configuration.BaseUri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));
        }

        /// <summary>
        /// This is the main method through which all api requests are made.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of the generic TResponse.</returns>
        internal static async Task<TResponse> Invoke<TResponse>(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
                var response = await DeserializeResponse<TResponse>(httpResponseMessage).ConfigureAwait(false);
                return response;
            }
            catch (CenturyLinkCloudServiceException ex)
            {
                ex.HttpRequestMessage = httpRequestMessage;
                ex.HttpResponseMessage = httpResponseMessage;
                throw ex;
            }
            catch (Exception ex)
            {
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage, ex);
                serviceException.HttpRequestMessage = httpRequestMessage;
                serviceException.HttpResponseMessage = httpResponseMessage;
                throw serviceException;
            }
        }

        private static async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage httpResponseMessage)
        {
            string apiMessage = null;

            Uri authenticationURL = httpResponseMessage.Headers.Location;
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(jsonString))
            {
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage);
                throw serviceException;
            }

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var deserializableJson = jsonString.CreateDeserializableJsonString();
                var result = JsonConvert.DeserializeObject<TResponse>(deserializableJson);
                return result;
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                JObject jsonObject = jsonString.TryParseJson();

                if (jsonObject != null)
                {
                    apiMessage = (string)jsonObject["message"];

                    var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage);

                    if (!string.IsNullOrEmpty(apiMessage))
                    {
                        serviceException.ApiMessage = apiMessage;
                    }

                    throw serviceException;
                }
            }

            return default(TResponse);
        }
    }
}
