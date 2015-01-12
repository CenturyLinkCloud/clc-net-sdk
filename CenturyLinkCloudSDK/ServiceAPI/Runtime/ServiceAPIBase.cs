using CenturyLinkCloudSDK.ServiceModels;
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
            where TResponse : IServiceModel
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(request.BaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.MediaType));

                HttpResponseMessage response = null;

                if (request.HttpMethod == HttpMethod.Get)
                {
                    response = await client.GetAsync(request.ServiceUri);
                }
                else if (request.HttpMethod == HttpMethod.Post)
                {                   
                    response = await client.PostAsJsonAsync(request.ServiceUri, request.RequestModel);
                }
                else if (request.HttpMethod == HttpMethod.Put)
                {
                    response = await client.PutAsJsonAsync(request.ServiceUri, request.RequestModel);
                }
                else if (request.HttpMethod == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(request.ServiceUri);
                }

                if (response.IsSuccessStatusCode)
                {
                    Uri authenticationURL = response.Headers.Location;
                    var result = await response.Content.ReadAsAsync<TResponse>();
                    return result;
                }

                return default(TResponse);
               
            }
        }
    }
}
