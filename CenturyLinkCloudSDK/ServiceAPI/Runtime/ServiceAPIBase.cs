using CenturyLinkCloudSDK.Extensions;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.Runtime
{
    public abstract class ServiceAPIBase
    {
        protected async Task<TResponse> Invoke<TRequest, TResponse>(TRequest request) 
            where TRequest : ServiceRequest 
            where TResponse : IServiceResponseModel
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(request.BaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.MediaType));

                if (Persistence.IsUserAuthenticated)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Persistence.UserInfo.BearerToken);
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

                if (response.IsSuccessStatusCode)
                {
                    var responseDeserialized = false;
                    Uri authenticationURL = response.Headers.Location;

                    try
                    {
                        var result = await response.Content.ReadAsAsync<TResponse>().ConfigureAwait(false);
                        return result;                    
                    }
                    catch(JsonSerializationException)
                    {
                        responseDeserialized = false;
                    }

                    if(!responseDeserialized)
                    {
                        var namedJsonArray = await RetryReadWithNamedArray(response).ConfigureAwait(false);
                        var result = JsonConvert.DeserializeObject<TResponse>(namedJsonArray);
                        return result;
                    }
                }

                return default(TResponse);              
            }
        }

        private async Task<string> RetryReadWithNamedArray(HttpResponseMessage response)
        {
            var stringResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return stringResult.CreateDeserializableJsonString();
        }
    }
}
