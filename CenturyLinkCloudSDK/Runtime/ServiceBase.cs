using CenturyLinkCloudSDK.ServiceModels;
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

        protected HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string serviceUri, string content)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, serviceUri);
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ServiceUris.JsonMediaType));

            if (!string.IsNullOrEmpty(content))
            {
                httpRequestMessage.Content = new StringContent(content, new UTF8Encoding(), Constants.ServiceUris.JsonMediaType);
            }

            if (authentication != null)
            {
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authentication.BearerToken);
            }

            return httpRequestMessage;
        }
    }
}
