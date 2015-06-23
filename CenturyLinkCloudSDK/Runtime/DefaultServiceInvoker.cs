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
        CenturyLinkCloudServiceException CreateDetailedException(HttpRequestMessage request, HttpResponseMessage response, Exception innerOrNull)
        {
            var message = Constants.ExceptionMessages.DefaultServiceExceptionMessage;
            var ex =
                innerOrNull == null ?
                    new CenturyLinkCloudServiceException(message) :
                    new CenturyLinkCloudServiceException(message, innerOrNull);
                
            ex.HttpRequestMessage = request;
            ex.HttpResponseMessage = response;
            
            if (request.Content != null)
            {
                ex.RequestContent = request.Content.ReadAsStringAsync().Result;
            }

            if (response.Content != null)
            {
                ex.ResponseContent = response.Content.ReadAsStringAsync().Result;
            }

            return ex;
        }

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
            var messageFormatter = Configuration.HttpMessageFormatter;

            using (HttpClient httpClient = new HttpClient(new NativeMessageHandler()))
            {
                httpClient.BaseAddress = new Uri(Configuration.BaseUri);
                
                HttpResponseMessage httpResponseMessage = null;

                try
                {
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return await messageFormatter.DeserializeResponse<TResponse>(httpResponseMessage).ConfigureAwait(false);
                    }                                        
                }catch(Exception ex)
                {
                    //sending failed or parsing the response did
                    throw CreateDetailedException(httpRequestMessage, httpResponseMessage, ex);                        
                }

                //request failed

                //This logic is necessary to prevent exceptions from being thrown when a resource is not found due to bad data in the system.
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(TResponse);
                }

                var detailedEx = CreateDetailedException(httpRequestMessage, httpResponseMessage, null);
                var errorMessageOrEmpty = await Configuration.HttpMessageFormatter.DeserializeErrorResponse(httpResponseMessage).ConfigureAwait(false);
                detailedEx.ApiMessage =
                    string.IsNullOrEmpty(errorMessageOrEmpty) ?
                        Constants.ExceptionMessages.DefaultServiceExceptionMessage :
                        errorMessageOrEmpty;
                throw detailedEx;                    
            }
        }        
    }
}
