using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.Extensions;
using Newtonsoft.Json.Linq;

namespace CenturyLinkCloudSDK.Runtime
{
    internal static class ServiceInvoker
    {
        private static HttpClient httpClient;

        static ServiceInvoker()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Constants.ServiceUris.ApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));
        }

        /// <summary>
        /// This is the main method through which all api requests are made. It serializes the data to JSON before making the api call,
        /// determines the appropriate Http method to call, and deserializes the response to a response class that it returns to the caller. 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns>An asynchronous Task of the generic TResponse which must implement IServiceResponse</returns>
        internal static async Task<TResponse> Invoke<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : ServiceRequest
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                httpResponseMessage = await SendRequest(request, cancellationToken).ConfigureAwait(false);
                var response = await DeserializeResponse<TResponse>(httpResponseMessage).ConfigureAwait(false);
                return response;
            }
            catch (CenturyLinkCloudServiceException serviceException)
            {
                serviceException = BuildUpServiceException(serviceException, request, httpResponseMessage);
                throw serviceException;
            }
            catch (Exception ex)
            {
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage, ex);
                serviceException = BuildUpServiceException(serviceException, request, httpResponseMessage);
                throw serviceException;
            }
        }

        private static async Task<HttpResponseMessage> SendRequest<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : ServiceRequest
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                if (request.BearerToken != null && httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);
                }

                httpResponseMessage = await httpClient.SendAsync<TRequest>(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage, ex);
                throw serviceException;
            }

            return httpResponseMessage;
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

        private static CenturyLinkCloudServiceException BuildUpServiceException(CenturyLinkCloudServiceException serviceException, ServiceRequest request, HttpResponseMessage httpResponseMessage)
        {
            serviceException.ServiceUri = request.ServiceUri;
            serviceException.HttpMethod = request.HttpMethod.ToString();
            serviceException.HttpResponseMessage = httpResponseMessage;

            return serviceException;
        }
    }
}
