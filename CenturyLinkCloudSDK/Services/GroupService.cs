﻿using CenturyLinkCloudSDK.Runtime;
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

            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Group.GetRecentActivity, Configuration.BaseUri), requestModel);
            var result = await serviceInvoker.Invoke<IEnumerable<Activity>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the group overview, which contains composite information from several different areas such as billing, assets, recent activities etc.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<GroupOverview> GetGroupOverview(string groupId)
        {
            return await GetGroupOverview(groupId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the group overview, which contains composite information from several different areas such as billing, assets, recent activities etc.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GroupOverview> GetGroupOverview(string groupId, CancellationToken cancellationToken)
        {
            var group = await GetGroup(groupId, cancellationToken).ConfigureAwait(false);

            if (group != null)
            {
                var serverIds = GetServerIds(group, new List<string>());
                var tasks = new List<Task>();

                BillingDetail billingTotals = null;
                TotalAssets totalAssets = null;
                DefaultSettings defaultSettings = null;
                IEnumerable<Activity> recentActivity = null;

                tasks.Add(Task.Run(async () => billingTotals = await group.GetBillingTotals(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => totalAssets = await GetTotalAssets(serverIds, cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => defaultSettings = await group.GetDefaultSettings(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => recentActivity = await GetRecentActivity(serverIds, cancellationToken).ConfigureAwait(false)));

                await Task.WhenAll(tasks);

                var groupOverview = new GroupOverview()
                {
                    Group = group,
                    BillingTotals = billingTotals,
                    TotalAssets = totalAssets,
                    DefaultSettings = defaultSettings,
                    RecentActivity = recentActivity
                };

                return groupOverview;
            }

            return null;
        }

        /// <summary>
        /// Gets the total assets for the group.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<TotalAssets> GetTotalAssets(List<string> serverIds)
        {
            return await GetTotalAssets(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        
        /// <summary>
        /// Optioonally recursive method that gets the serverIds of the data center root group and all subgroups.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="serverIds"></param>
        /// <param name="IsRecursive"></param>
        /// <returns></returns>
        public List<string> GetServerIds(Group group, List<string> serverIds, bool IsRecursive = true)
        {
            var groupServerIds = group.GetServerIds();

            if (groupServerIds != null)
            {
                serverIds.AddRange(groupServerIds);
            }

            if (IsRecursive)
            {
                foreach (var subgroup in group.Groups)
                {
                    serverIds = GetServerIds(subgroup, serverIds, IsRecursive);
                }
            }

            return serverIds;
        }

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
        /// Gets the default settings.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<DefaultSettings> GetDefaultSettingsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<DefaultSettings>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }
        /// <summary>
        /// Gets the default settings that are inherited by the Data Center.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<DefaultSettings> GetDefaultSettings(string groupId)
        {
            return await GetDefaultSettings(groupId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the default settings that are inherited by the Data Center.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DefaultSettings> GetDefaultSettings(string groupId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Group.GetDefaultSettings, Configuration.BaseUri, authentication.AccountAlias, groupId);            
            return await GetDefaultSettingsByLink(uri, cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Gets the details for a individual server group and any sub-groups and servers that it contains by hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<Group> GetGroupByLink(string uri)
        {
            return await GetGroupByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

*/
    }
}
