using CenturyLinkCloudSDK.Services.Runtime;
using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Groups.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with server groups.
    /// </summary>
    public class GroupService : ServiceBase
    {
        private AuthenticationInfo userAuthentication;

        internal GroupService(AuthenticationInfo userAuthentication)
        {
            this.userAuthentication = userAuthentication;
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains. 
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="groupId"></param>
        /// <returns>An asynchronous Task of Group.</returns>
        public async Task<Group> GetGroup(string groupId)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format("https://api.tier3.com/v2/groups/{0}/{1}", userAuthentication.AccountAlias, groupId),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetGroupResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains by hypermedia link.
        /// </summary>
        /// <param name="hypermediaLink"></param>
        /// <returns>An asynchronous Task of Group.</returns>
        public async Task<Group> GetGroupByHyperLink(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = Constants.ApiBaseAddress + hypermediaLink,
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetGroupResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }
    }
}
