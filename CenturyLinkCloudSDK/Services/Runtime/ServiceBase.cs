using CenturyLinkCloudSDK.Extensions;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using Newtonsoft.Json;
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
        internal async Task<TResponse> Invoke<TRequest, TResponse>(TRequest request) 
            where TRequest : ServiceRequest 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.ApiBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonMediaType));

                if (request.BearerToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);
                }

                HttpResponseMessage response = null;

                if (request.HttpMethod == HttpMethod.Get)
                {
                    response = await client.GetAsync(request.ServiceUri).ConfigureAwait(false);
                }
                else if (request.HttpMethod == HttpMethod.Post)
                {
                    if (request.RequestModel.UnNamedArray != null)
                    {
                        response = await client.PostAsJsonAsync(request.ServiceUri, request.RequestModel.UnNamedArray).ConfigureAwait(false);
                    }
                    else
                    {
                        response = await client.PostAsJsonAsync(request.ServiceUri, request.RequestModel).ConfigureAwait(false);
                    }
                }
                else if (request.HttpMethod == HttpMethod.Put)
                {
                    if (request.RequestModel.UnNamedArray != null)
                    {
                        response = await client.PutAsJsonAsync(request.ServiceUri, request.RequestModel.UnNamedArray).ConfigureAwait(false);
                    }
                    else
                    {
                        response = await client.PutAsJsonAsync(request.ServiceUri, request.RequestModel).ConfigureAwait(false);
                    }
                }
                else if (request.HttpMethod == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(request.ServiceUri).ConfigureAwait(false);
                }

                Uri authenticationURL = response.Headers.Location;

                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if(!string.IsNullOrEmpty(jsonString))
                {
                    var namedObject = jsonString.CreateDeserializableJsonString();
                    var result = JsonConvert.DeserializeObject<TResponse>(namedObject);
                    return result;
                }

                return default(TResponse);              
            }
        }
    }
}
