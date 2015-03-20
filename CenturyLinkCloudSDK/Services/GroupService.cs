using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.Group;
using CenturyLinkCloudSDK.ServiceModels.Responses.Groups;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds)
        {

            return await GetRecentActivity(referenceIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, CancellationToken cancellationToken)
        {

            return await GetRecentActivity(referenceIds, 10, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordCountLimit)
        {

            return await GetRecentActivity(referenceIds, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> accounts, IEnumerable<string> referenceIds, int recordCountLimit)
        {
            return await GetRecentActivity(accounts, referenceIds, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <param name="referenceIds"></param>
        /// <param name="recordCountLimit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordCountLimit, CancellationToken cancellationToken)
        {
            var accounts = new List<string>();
            accounts.Add(authentication.AccountAlias);

            return await GetRecentActivity(accounts, referenceIds, recordCountLimit, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> accounts, IEnumerable<string> referenceIds, int recordCountLimit, CancellationToken cancellationToken)
        {
            var requestModel = new GetRecentActivityRequest()
            {
                EntityTypes = new List<string>() { Constants.EntityTypes.Server, Constants.EntityTypes.Group },
                ReferenceIds = referenceIds,
                Accounts = accounts,
                Limit = recordCountLimit
            };

            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Group.GetRecentActivity, Configuration.BaseUri), requestModel);
            var result = await ServiceInvoker.Invoke<IEnumerable<Activity>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Recursive method that gets the serverIds of the data center root group and all subgroups.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        internal List<string> GetServerIds(Group group, List<string> serverIds)
        {
            var groupServerIds = group.GetServerIds();

            if (groupServerIds != null)
            {
                serverIds.AddRange(groupServerIds);
            }

            foreach (var subgroup in group.Groups)
            {
                serverIds = GetServerIds(subgroup, serverIds);
            }

            return serverIds;
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
    }
}
