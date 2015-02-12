using CenturyLinkCloudSDK.Extensions;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    /// <summary>
    /// This class is the base class that all service api classes must inherit from.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// This is the main method through which all api requests are made. It serializes the data to JSON before making the api call,
        /// determines the appropriate Http method to call, and deserializes the response to a response class that it returns to the caller. 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns>An asynchronous Task of the generic TResponse which must implement IServiceResponse</returns>
        internal async Task<TResponse> Invoke<TRequest, TResponse>(TRequest request) where TRequest : ServiceRequest
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                httpResponseMessage = await MakeRequest(request).ConfigureAwait(false);
                var response = await DeserializeResponse<TResponse>(httpResponseMessage).ConfigureAwait(false);
                return response;
            }
            catch (CenturyLinkCloudServiceException serviceException)
            {
                //TODO: Perform exception logging operation.
                serviceException = BuildUpServiceException(serviceException, request, httpResponseMessage);
                throw serviceException;
            }
            catch (Exception ex)
            {
                //TODO: Perform exception logging operation.
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage, ex);
                serviceException = BuildUpServiceException(serviceException, request, httpResponseMessage);
                throw serviceException;
            }
        }

        private async Task<HttpResponseMessage> MakeRequest<TRequest>(TRequest request) where TRequest : ServiceRequest
        {
            HttpResponseMessage httpResponseMessage = null;

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Constants.ServiceUris.ApiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));

                    if (request.BearerToken != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);
                    }

                    httpResponseMessage = await client.SendAsync<TRequest>(request).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage, ex);
                    throw serviceException;
                }

                return httpResponseMessage;
            }
        }

        private async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage httpResponseMessage)
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

                if(jsonObject != null)
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

        private CenturyLinkCloudServiceException BuildUpServiceException(CenturyLinkCloudServiceException serviceException, ServiceRequest request, HttpResponseMessage httpResponseMessage)
        {
            serviceException.ServiceUri = request.ServiceUri;
            serviceException.HttpMethod = request.HttpMethod.ToString();
            serviceException.HttpResponseMessage = httpResponseMessage;

            return serviceException;
        }
    }
}
