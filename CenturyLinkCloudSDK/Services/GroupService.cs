using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.Groups;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with server groups.
    /// </summary>
    public class GroupService : ServiceBase
    {
        internal GroupService(Authentication authentication)
            : base(authentication)
        {
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains. 
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<Group> GetGroup(string groupId)
        {
            return await GetGroup(groupId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Group> GetGroup(string groupId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Group.GetGroup, Configuration.BaseUri, authentication.AccountAlias, groupId);
            return await GetGroupByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        public async Task<GroupBillingDetail> GetGroupBillingDetails(string groupId)
        {
            return await GetGroupBillingDetails(groupId, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<GroupBillingDetail> GetGroupBillingDetails(string groupId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Group.GetGroupBillingDetails, Configuration.BaseUri, authentication.AccountAlias, groupId);
            return await GetGroupBillingDetailsByLink(uri, cancellationToken).ConfigureAwait(false);
        }

       /// <summary>
       /// Gets the details for a individual server group and any sub-groups and servers that it contains by hypermedia link.
       /// </summary>
       /// <param name="uri"></param>
       /// <returns></returns>
        internal async Task<Group> GetGroupByLink(string uri)
        {
            return await GetGroupByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains by hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<Group> GetGroupByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<Group>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            result.Authentication = authentication;

            return result;
        }

        internal async Task<GroupBillingDetail> GetGroupBillingDetailsByLink(string uri)
        {
            return await GetGroupBillingDetailsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        internal async Task<GroupBillingDetail> GetGroupBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<GroupBillingDetail>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
