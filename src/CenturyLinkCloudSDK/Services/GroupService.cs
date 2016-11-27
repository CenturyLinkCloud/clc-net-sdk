using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.Group;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.Extensions;
using System.Linq;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with server groups.
    /// </summary>
    public class GroupService : ServiceBase
    {        
        ServerService serverService;
        internal GroupService(Authentication authentication, IServiceInvoker serviceInvoker, ServerService serverService)
            : base(authentication, serviceInvoker)
        {
            this.serverService = serverService;
        }

        void SetInternalGroupProperties(Group group)
        {
            group.ServerService = serverService;
            group.GroupService = this;
        }

        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="createGroupRequest">The details for the group to create</param>
        /// <returns>The newly created group</returns>
        public Task<Group> CreateGroup(CreateGroupRequest createGroupRequest)
        {
            return CreateGroup(createGroupRequest, CancellationToken.None);
        }

        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="createGroupRequest">The details for the group to create</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The newly created group</returns>
        public async Task<Group> CreateGroup(CreateGroupRequest createGroupRequest, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Group.CreateGroup, Configuration.BaseUri, authentication.AccountAlias), createGroupRequest);
            var result = await serviceInvoker.Invoke<Group>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            SetInternalGroupProperties(result);

            return result;
        }

        /// <summary>
        /// Enqueues a job that deletes the indicated group
        /// </summary>
        /// <param name="groupId">The id of the group to delete</param>
        /// <returns>A link for querying status of the job</returns>
        public Task<Link> DeleteGroup(string groupId)
        {
            return DeleteGroup(groupId, CancellationToken.None);
        }

        /// <summary>
        /// Enqueues a job that deletes the indicated group
        /// </summary>
        /// <param name="groupId">The id of the group to delete</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A link for querying status of the job</returns>
        public Task<Link> DeleteGroup(string groupId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Group.GetGroup, Configuration.BaseUri, authentication.AccountAlias, groupId);
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Delete, uri);

            return serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains. 
        /// </summary>
        /// <param name="groupId">The id of the group</param>
        /// <returns>The group</returns>
        public Task<Group> GetGroup(string groupId)
        {
            return GetGroup(groupId, CancellationToken.None);
        }

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains.
        /// </summary>
        /// <param name="groupId">The id of the group</param>        
        /// <returns>The group</returns>
        public Task<Group> GetGroup(string groupId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Group.GetGroup, Configuration.BaseUri, authentication.AccountAlias, groupId);
            return GetGroupByLink(uri, cancellationToken);
        }
        
        internal async Task<Group> GetGroupByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<Group>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            SetInternalGroupProperties(result);

            return result;
        }

        internal Task<DefaultSettings> GetDefaultSettingsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<DefaultSettings>(httpRequestMessage, cancellationToken);
        }        
    }
}
