using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
using CenturyLinkCloudSDK.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Group
    {
        Lazy<IEnumerable<Link>> serverLinks;
        Lazy<Link> billingLink;
        Lazy<Link> defaultsLink;
        Lazy<Link> createServerLink;        

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int ServersCount { get; set; }
        public ComputeLimits Limits { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public ChangeInfo ChangeInfo { get; set; }        

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Group()
        {
            serverLinks = new Lazy<IEnumerable<Link>>(()=>
            {
                return Links.Where(l => String.Equals(l.Rel, "server", StringComparison.CurrentCultureIgnoreCase)).ToList();
            });

            billingLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "billing", StringComparison.CurrentCultureIgnoreCase));
            });

            defaultsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "defaults", StringComparison.CurrentCultureIgnoreCase));
            });

            createServerLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "createServer", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        /// <summary>
        /// Determines if this Group has servers
        /// </summary>
        public bool HasServers
        {
            get { return serverLinks.Value.Count() > 0; }
        }        

        /// <summary>
        /// Determines if this group has default settings
        /// </summary>
        /// <returns></returns>
        public bool HasDefaults
        {
            get { return defaultsLink.Value != null; }
        }

        /// <summary>
        /// Determines if this group has billing details
        /// internal because we are using BillingService to handle all billing concerns
        /// </summary>
        /// <returns></returns>
        internal bool HasBillingDetails
        {
            get { return billingLink.Value != null; }
        }

        internal string BillingDetailsLink
        {
            get { return HasBillingDetails ? billingLink.Value.Href : null; }
        }

        /// <summary>
        /// Determines if the create server functionality is available.
        /// </summary>
        /// <returns></returns>
        public bool CanCreateServer
        {
            get { return createServerLink.Value != null; }
        }

        internal void AppendContainedGroupAndServerIds(List<string> ids)
        {
            ids.Add(Id);
            if(HasServers)
            {
                ids.AddRange(GetServerIds(includeSubGroups: false));
            }
            
            foreach(Group g in Groups)
            {
                g.AppendContainedGroupAndServerIds(ids);
            }
        }
       
        /// <summary>
        /// Returns the ids of the servers in this group and optionally all subgroups
        /// </summary>        
        /// <param name="includeSubGroups">Whether to include the servers from this group only or all sub groups</param>
        public IEnumerable<string> GetServerIds(bool includeSubGroups)
        {
            var serverIds =
                new List<string>(
                    !HasServers ?
                        Enumerable.Empty<string>() :
                        serverLinks
                            .Value
                            .Select(l => l.Id)
                            .ToList());

            if(includeSubGroups)
            {
                serverIds.AddRange(
                    Groups
                        .SelectMany(
                            g => g.GetServerIds(includeSubGroups: true)));
            }

            return serverIds;
        }

        internal GroupService GroupService { get; set; }
        internal ServerService ServerService { get; set; }
        
        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <param name="includeSubGroups">Whether to include servers in all subgroups or not</param>
        /// <returns>The servers in this group</returns>
        public Task<IEnumerable<Server>> GetServers(bool includeSubGroups)
        {
            return GetServers(includeSubGroups, CancellationToken.None);
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>        
        /// <param name="includeSubGroups">Whether to include servers in all subgroups or not</param>
        /// <returns>The servers in this group</returns>
        public Task<IEnumerable<Server>> GetServers(bool includeSubGroups, CancellationToken cancellationToken)
        {
            return ServerService.GetServers(GetServerIds(includeSubGroups), cancellationToken);
        }

        /// <summary>
        /// Gets the total assets for the group.
        /// </summary>
        /// <returns>The total assets for all groups and subgroups</returns>
        public Task<TotalAssets> GetTotalAssets()
        {
            return GetTotalAssets(CancellationToken.None);
        }

        /// <summary>
        /// Gets the total assets for the group.
        /// </summary>        
        /// <returns>The total assets for all groups and subgroups</returns>
        public async Task<TotalAssets> GetTotalAssets(CancellationToken cancellationToken)
        {
            var totalAssets = new TotalAssets();

            var servers = await GetServers(includeSubGroups: true, cancellationToken: cancellationToken).ConfigureAwait(false);
            totalAssets.Servers = servers.Count();

            foreach (var s in servers)
            {
                if (s != null)
                {
                    if (s.Details != null)
                    {
                        totalAssets.Cpus += s.Details.Cpu;
                        totalAssets.MemoryGB += s.Details.MemoryMB;
                        totalAssets.StorageGB += s.Details.StorageGB;
                    }
                }
            }

            //The memory values we get for the servers is in MB so we need to convert to GB to display.
            totalAssets.Memory = totalAssets.MemoryGB.ConvertAssetMeasure(Constants.Metrics.MegaBytes);

            //Just in case we do that for StorageGB as well.
            totalAssets.Storage = totalAssets.StorageGB.ConvertAssetMeasure(Constants.Metrics.GigaBytes);

            return totalAssets;
        }

        /// <summary>
        /// Get the default settings.
        /// </summary>
        /// <returns>The default settings</returns>
        public Task<DefaultSettings> GetDefaultSettings()
        {
            return GetDefaultSettings(CancellationToken.None);
        }

        /// <summary>
        /// Gets the default settings.
        /// </summary>
        /// <returns>The default settings</returns>
        public Task<DefaultSettings> GetDefaultSettings(CancellationToken cancellationToken)
        {
            if (!HasDefaults)
            {
                return null;
            }

            return GroupService.GetDefaultSettingsByLink(string.Format("{0}{1}", Configuration.BaseUri, defaultsLink.Value.Href), cancellationToken);
        }

        /*
             
        /// <summary>
        /// Returns the server Ids for all groups and subgroups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetServerIds()
        {
            return GetServerIds(CancellationToken.None);
        }
        
        /// <summary>
        /// Gets the billing details.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupBillingDetail> GetBillingDetails()
        {
            return await GetBillingDetails(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GroupBillingDetail> GetBillingDetails(CancellationToken cancellationToken)
        {
            if (!HasBillingDetails())
            {
                return null;
            }

            var billingService = Configuration.ServiceResolver.Resolve<BillingService>(Authentication);
            var billingDetails = await billingService.GetGroupBillingDetailsByLink(string.Format("{0}{1}", Configuration.BaseUri, billingLink.Value.Href), cancellationToken).ConfigureAwait(false);
            return billingDetails;
        }

        /// <summary>
        /// Gets the billing totals.
        /// </summary>
        /// <returns></returns>
        public async Task<BillingDetail> GetBillingTotals()
        {
            return await GetBillingTotals(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing totals.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetBillingTotals(CancellationToken cancellationToken)
        {
            if (!HasBillingDetails())
            {
                return null;
            }

            var billingService = Configuration.ServiceResolver.Resolve<BillingService>(Authentication);
            var billingTotals = await billingService.GetGroupBillingTotalsByLink(string.Format("{0}{1}", Configuration.BaseUri, billingLink.Value.Href), cancellationToken).ConfigureAwait(false);
            return billingTotals;
        }

        */
    }
}
