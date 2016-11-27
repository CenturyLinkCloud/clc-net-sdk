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
        protected IServiceInvoker serviceInvoker;

        protected ServiceBase(Authentication authentication, IServiceInvoker serviceInvoker)
        {
            this.authentication = authentication;
            this.serviceInvoker = serviceInvoker;
        }


        /// <summary>
        /// Creates an HttpRequestMessage without content.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="serviceUri"></param>
        /// <returns></returns>
        protected HttpRequestMessage CreateAuthorizedHttpRequestMessage(HttpMethod httpMethod, string serviceUri)
        {
            return CreateAuthorizedHttpRequestMessage(httpMethod, serviceUri, null);
        }

        /// <summary>
        /// Creates an HttpRequestMessage with content.
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="httpMethod"></param>
        /// <param name="serviceUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        protected HttpRequestMessage CreateAuthorizedHttpRequestMessage(HttpMethod httpMethod, string serviceUri, object content)
        {
            return
                Configuration
                    .HttpMessageFormatter
                    .CreateHttpRequestMessage(httpMethod, serviceUri, authentication, content);            
        }
    }
}
