using CenturyLinkCloudSDK.Extensions;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    internal static class ServiceInvoker
    {
        /// <summary>
        /// This is the main method through which all api requests are made.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of the generic TResponse.</returns>
        internal static async Task<TResponse> Invoke<TResponse>(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Configuration.BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));

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

                    if (httpRequestMessage.Content != null)
                    {
                        ex.RequestContent = httpRequestMessage.Content.ReadAsStringAsync().Result;
                    }

                    if (httpResponseMessage.Content != null)
                    {
                        ex.ResponseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    }

                    throw ex;
                }
                catch (Exception ex)
                {
                    var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.DefaultServiceExceptionMessage, ex);
                    serviceException.HttpRequestMessage = httpRequestMessage;
                    serviceException.HttpResponseMessage = httpResponseMessage;

                    if (httpRequestMessage.Content != null)
                    {
                        serviceException.RequestContent = httpRequestMessage.Content.ReadAsStringAsync().Result;
                    }

                    if (httpResponseMessage.Content != null)
                    {
                        serviceException.ResponseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    }

                    throw serviceException;
                }
            }
        }


        private static async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage httpResponseMessage)
        {
            string apiMessage = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<TResponse>().ConfigureAwait(false);
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.DefaultServiceExceptionMessage);
                
                var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                JObject jsonObject = content.TryParseJson();

                if (jsonObject != null)
                {
                    apiMessage = (string)jsonObject["message"];

                    if (!string.IsNullOrEmpty(apiMessage))
                    {
                        serviceException.ApiMessage = apiMessage;
                    }
                }

                throw serviceException;
            }

            return default(TResponse);
        }
    }
}
