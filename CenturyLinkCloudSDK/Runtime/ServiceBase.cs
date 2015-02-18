using CenturyLinkCloudSDK.ServiceModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CenturyLinkCloudSDK.Runtime
{
    public abstract class ServiceBase
    {
        protected Authentication authentication;

        protected ServiceBase(Authentication authentication)
        {
            this.authentication = authentication;
        }


        /// <summary>
        /// Creates an HttpRequestMessage without content.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="serviceUri"></param>
        /// <returns></returns>
        protected HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string serviceUri)
        {
            return CreateHttpRequestMessage<Object>(httpMethod, serviceUri, null);
        }

        /// <summary>
        /// Creates an HttpRequestMessage with content.
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="httpMethod"></param>
        /// <param name="serviceUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        protected HttpRequestMessage CreateHttpRequestMessage<TContent>(HttpMethod httpMethod, string serviceUri, TContent content)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, serviceUri);
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));

            try
            {
                if (content != null)
                {
                    var serializedContent = JsonConvert.SerializeObject(content);
                    httpRequestMessage.Content = new StringContent(serializedContent, new UTF8Encoding(), Constants.ServiceUris.JsonMediaType);
                }

                if (authentication != null)
                {
                    httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authentication.BearerToken);
                }
            }
            catch(Exception ex)
            {
                var serviceException = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.ServiceExceptionMessage, ex);
                serviceException.HttpRequestMessage = httpRequestMessage;
                throw serviceException;
            }

            return httpRequestMessage;
        }
    }
}
