using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Queues.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Queues : ServiceAPIBase
    {
        public async Task<Queue> GetStatus(string accountAlias, string statusId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/status/{1}", accountAlias, statusId),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetStatusResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Queue;
                return response;
            }

            return null;
        }

        public async Task<Queue> GetStatus(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = hypermediaLink,
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetStatusResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Queue;
                return response;
            }

            return null;
        }
    }
}
