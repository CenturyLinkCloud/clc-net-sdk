using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.Groups;
using CenturyLinkCloudSDK.Runtime;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with server groups.
    /// </summary>
    public class GroupService
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
            return await GetGroup(groupId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of Group.</returns>
        public async Task<Group> GetGroup(string groupId, CancellationToken cancellationToken)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Group.GetGroup, userAuthentication.AccountAlias, groupId),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetGroupResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

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
                ServiceUri = Constants.ServiceUris.ApiBaseAddress + hypermediaLink,
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetGroupResponse>(serviceRequest, CancellationToken.None).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }
    }
}
