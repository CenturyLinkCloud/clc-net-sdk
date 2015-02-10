using CenturyLinkCloudSDK.Extensions;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services.Runtime
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

                    if (request.HttpMethod == HttpMethod.Get)
                    {
                        httpResponseMessage = await client.GetAsync(request.ServiceUri).ConfigureAwait(false);
                    }
                    else if (request.HttpMethod == HttpMethod.Post)
                    {
                        if (request.RequestModel.UnNamedArray != null)
                        {
                            httpResponseMessage = await client.PostAsJsonAsync(request.ServiceUri, request.RequestModel.UnNamedArray).ConfigureAwait(false);
                        }
                        else
                        {
                            httpResponseMessage = await client.PostAsJsonAsync(request.ServiceUri, request.RequestModel).ConfigureAwait(false);
                        }
                    }
                    else if (request.HttpMethod == HttpMethod.Put)
                    {
                        if (request.RequestModel.UnNamedArray != null)
                        {
                            httpResponseMessage = await client.PutAsJsonAsync(request.ServiceUri, request.RequestModel.UnNamedArray).ConfigureAwait(false);
                        }
                        else
                        {
                            httpResponseMessage = await client.PutAsJsonAsync(request.ServiceUri, request.RequestModel).ConfigureAwait(false);
                        }
                    }
                    else if (request.HttpMethod == HttpMethod.Delete)
                    {
                        httpResponseMessage = await client.DeleteAsync(request.ServiceUri).ConfigureAwait(false);
                    }
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

            var deserializableJson = jsonString.CreateDeserializableJsonString();
            var result = JsonConvert.DeserializeObject<TResponse>(deserializableJson);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                //If it is a server power operation we just return the result becasue it contains
                //status information and error messages for each server attempted to be operated on.
                if (result.GetType() != typeof(ServerPowerOpsResponse))
                {
                    JObject json = JObject.Parse(jsonString);
                    apiMessage = (string)json["message"];

                    var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage);

                    if (!string.IsNullOrEmpty(apiMessage))
                    {
                        serviceException.ApiMessage = apiMessage;
                    }

                    throw serviceException;
                }
            }

            return result;
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
