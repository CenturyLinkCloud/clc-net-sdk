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

        /*        
        /// <summary>
        /// Gets the group hierarchy by GroupId with nested groups and servers optional.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="includeServers"></param>
        /// <returns></returns>
        public async Task<GroupHierarchy> GetGroupHierarchy(string groupId, bool includeServers)
        {
            return await GetGroupHierarchy(groupId, includeServers, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the group hierarchy by GroupId with nested groups and servers optional.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="includeServers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GroupHierarchy> GetGroupHierarchy(string groupId, bool includeServers, CancellationToken cancellationToken)
        {
            var group = await GetGroup(groupId).ConfigureAwait(false);
            return await GetGroupHierarchy(group, includeServers, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the group hierarchy by Group with nested groups and servers optional.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="includeServers"></param>
        /// <returns></returns>
        public async Task<GroupHierarchy> GetGroupHierarchy(Group group, bool includeServers)
        {
            return await GetGroupHierarchy(group, new GroupHierarchy(), includeServers, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the group hierarchy by Group with nested groups and servers optional.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="includeServers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GroupHierarchy> GetGroupHierarchy(Group group, bool includeServers, CancellationToken cancellationToken)
        {
            return await GetGroupHierarchy(group, new GroupHierarchy(), includeServers, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Recursive method that loops through all nested groups within a group and returns the whole hierarchy tree with servers optional.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="groupHierarchy"></param>
        /// <param name="includeServers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<GroupHierarchy> GetGroupHierarchy(Group group, GroupHierarchy groupHierarchy, bool includeServers, CancellationToken cancellationToken)
        {
            groupHierarchy.GroupId = group.Id;
            groupHierarchy.GroupName = group.Name;

            group.Authentication = authentication;

            if (includeServers)
            {
                var groupServers = await group.GetServers(cancellationToken).ConfigureAwait(false);

                if (groupServers != null)
                {
                    foreach (var server in groupServers)
                    {
                        var serverState = new ServerState()
                        {
                            ServerId = server.Id,
                            ServerName = server.Name,
                            PowerState = server.Details.PowerState,
                            InMaintenanceMode = server.Details.InMaintenanceMode
                        };

                        groupHierarchy.Servers.Add(serverState);
                    }
                }
            }

            foreach (var subgroup in group.Groups)
            {
                if (subgroup.CanCreateServer())
                {
                    groupHierarchy.Groups.Add(await GetGroupHierarchy(subgroup, new GroupHierarchy(), includeServers, cancellationToken).ConfigureAwait(false));
                }
            }

            return groupHierarchy;
        }

*/
    }
}
