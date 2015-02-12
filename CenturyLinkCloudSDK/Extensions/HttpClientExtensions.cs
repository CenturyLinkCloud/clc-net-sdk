using CenturyLinkCloudSDK.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Extensions
{
    internal static class HttpClientExtensions
    {
        /// <summary>
        /// This extension determines which Http client method to call based on the request method.
        /// It also determines if the request model is a serializable object or an unnamed array and it passes the appropriate structure.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static async Task<HttpResponseMessage> SendAsync<TRequest>(this HttpClient httpClient, TRequest request) where TRequest : ServiceRequest
        {
            HttpResponseMessage httpResponseMessage = null;

            if (request.HttpMethod == HttpMethod.Get)
            {
                httpResponseMessage = await httpClient.GetAsync(request.ServiceUri).ConfigureAwait(false);
            }
            else if (request.HttpMethod == HttpMethod.Post)
            {
                if (request.RequestModel.UnNamedArray != null)
                {
                    httpResponseMessage = await httpClient.PostAsJsonAsync(request.ServiceUri, request.RequestModel.UnNamedArray).ConfigureAwait(false);
                }
                else
                {
                    httpResponseMessage = await httpClient.PostAsJsonAsync(request.ServiceUri, request.RequestModel).ConfigureAwait(false);
                }
            }
            else if (request.HttpMethod == HttpMethod.Put)
            {
                if (request.RequestModel.UnNamedArray != null)
                {
                    httpResponseMessage = await httpClient.PutAsJsonAsync(request.ServiceUri, request.RequestModel.UnNamedArray).ConfigureAwait(false);
                }
                else
                {
                    httpResponseMessage = await httpClient.PutAsJsonAsync(request.ServiceUri, request.RequestModel).ConfigureAwait(false);
                }
            }
            else if (request.HttpMethod == HttpMethod.Delete)
            {
                httpResponseMessage = await httpClient.DeleteAsync(request.ServiceUri).ConfigureAwait(false);
            }

            return httpResponseMessage;
        }
    }
}
