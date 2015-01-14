using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Server.Requests;
using CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Server: ServiceAPIBase
    {
        public async Task<GetServerResponse> GetServer(string accountAlias, string serverId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("v2/servers/{0}/{1}", accountAlias, serverId),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetServerResponse>(serviceRequest).ConfigureAwait(false);

            return result;
        }
    }
}
