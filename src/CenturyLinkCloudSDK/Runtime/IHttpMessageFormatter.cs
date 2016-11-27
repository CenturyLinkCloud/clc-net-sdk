using CenturyLinkCloudSDK.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    public interface IHttpMessageFormatter
    {
        HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string serviceUri, Authentication authentication, object content);
        Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response);
        Task<string> DeserializeErrorResponse(HttpResponseMessage response);
    }    
}
