using CenturyLinkCloudSDK.Extensions;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    internal class DefaultServiceInvoker : IServiceInvoker
    {
        /// <summary>
        /// This is the main method through which all api requests are made.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of the generic TResponse.</returns>
        public async Task<TResponse> Invoke<TResponse>(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
            where TResponse : class
        {
            using (HttpClient httpClient = new HttpClient(new NativeMessageHandler()))
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
            var apiMessage = new StringBuilder();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<TResponse>().ConfigureAwait(false);
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

                //This logic is necessary to prevent exceptions from being thrown when a resource is not found due to bad data in the system.
                if(httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(TResponse);
                }

                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.DefaultServiceExceptionMessage);
                var apiError = await DeserializeApiErrorMessage(httpResponseMessage).ConfigureAwait(false);

                if(apiError != null)
                {
                    if(!string.IsNullOrEmpty(apiError.Message))
                    {
                        apiMessage.Append(apiError.Message);
                    }

                    if(apiError.ModelState != null)
                    {
                        if(apiError.ModelState.Count > 0)
                        {
                            foreach(var modelProperty in apiError.ModelState)
                            {
                                foreach(var errorMessage in modelProperty.Value)
                                {
                                    apiMessage.AppendLine(errorMessage);
                                }
                            }
                        }
                    }

                }

                if (apiMessage.Length == 0)
                {
                    apiMessage.Append(Constants.ExceptionMessages.DefaultServiceExceptionMessage);
                }

                serviceException.ApiMessage = apiMessage.ToString();

                throw serviceException;
            }

            return default(TResponse);
        }

        private static async Task<ApiError> DeserializeApiErrorMessage(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                JObject jsonObject = content.TryParseJson();

                if (jsonObject != null)
                {
                    return await httpResponseMessage.Content.ReadAsAsync<ApiError>().ConfigureAwait(false);
                }
            }

            return null;
        }
    }
}
