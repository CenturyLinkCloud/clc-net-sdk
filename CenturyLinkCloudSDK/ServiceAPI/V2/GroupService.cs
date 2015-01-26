using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Groups.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    /// <summary>
    /// This class contains operations associated with server groups.
    /// </summary>
    public class GroupService : ServiceBase
    {
        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains. 
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="groupId"></param>
        /// <returns>An asynchronous Task of Group.</returns>
        public async Task<Group> GetGroup(string accountAlias, string groupId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
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

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains by hypermedia link.
        /// </summary>
        /// <param name="hypermediaLink"></param>
        /// <returns>An asynchronous Task of Group.</returns>
        public async Task<Group> GetGroup(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = Constants.API_BASE_ADDRESS + hypermediaLink,
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
