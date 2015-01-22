using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Groups.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Groups : ServiceAPIBase
    {
        public async Task<Group> GetGroup(string accountAlias, string groupId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/groups/{0}/{1}", accountAlias, groupId),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetGroupResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Group;
                return response;
            }

            return null;
        }

        public async Task<Group> GetGroup(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = hypermediaLink,
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetGroupResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Group;
                return response;
            }

            return null;
        }
    }
}
