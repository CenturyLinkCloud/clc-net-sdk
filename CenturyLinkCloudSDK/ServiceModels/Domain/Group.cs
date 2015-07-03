using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
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
                ids.AddRange(ServerIds);
            }
            
            foreach(Group g in Groups)
            {
                g.AppendContainedGroupAndServerIds(ids);
            }
        }
        
        /// <summary>
        /// Returns the ids of the servers in this group
        /// </summary>
        public IEnumerable<string> ServerIds
        {
            get
            {
                return
                    !HasServers ?
                        Enumerable.Empty<string>() :
                        serverLinks
                            .Value
                            .Select(l => l.Id)
                            .ToList();
            }
        }

        internal ServerService ServerService { get; set; }
        
        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <returns>The servers in this group</returns>
        public Task<IEnumerable<Server>> GetServers()
        {
            return GetServers(CancellationToken.None);
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>        
        /// <returns>The servers in this group</returns>
        public Task<IEnumerable<Server>> GetServers(CancellationToken cancellationToken)
        {
            return ServerService.GetServers(ServerIds, cancellationToken);
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

        /// <summary>
        /// Get the default settings.
        /// </summary>
        /// <returns></returns>
        public async Task<DefaultSettings> GetDefaultSettings()
        {
            return await GetDefaultSettings(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the default settings.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DefaultSettings> GetDefaultSettings(CancellationToken cancellationToken)
        {
            if (!HasDefaults())
            {
                return null;
            }

            var dataCenterService = Configuration.ServiceResolver.Resolve<DataCenterService>(Authentication);
            var defaultSettings = await dataCenterService.GetDefaultSettingsByLink(string.Format("{0}{1}", Configuration.BaseUri, defaultsLink.Value.Href), cancellationToken).ConfigureAwait(false);
            return defaultSettings;
        }*/
    }
}
