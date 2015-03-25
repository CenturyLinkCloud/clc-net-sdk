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
        private Lazy<IEnumerable<Link>> serverLinks;

        private Lazy<Link> billingLink;

        private Lazy<Link> defaultsLink;

        public Authentication Authentication { get; set; }

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
        }

        /// <summary>
        /// Determines if this Group has servers by examining the Links collection.
        /// </summary>
        private bool HasServers()
        {
            return serverLinks.Value.Count() > 0 ? true : false;
        }

        /// <summary>
        /// Determines if this group has billing details based on the Links collection.
        /// </summary>
        /// <returns></returns>
        private bool HasBillingDetails()
        {
            return billingLink.Value != null ? true : false;
        }

        /// <summary>
        /// Determines if this group has default settings based on the links collection.
        /// </summary>
        /// <returns></returns>
        private bool HasDefaults()
        {
            return defaultsLink.Value != null ? true : false;
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Server>> GetServers()
        {
            return await GetServers(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Server>> GetServers(CancellationToken cancellationToken)
        {
            var servers = new List<Server>();

            if (!HasServers())
            {
                return null;
            }

            var serverService = new ServerService(Authentication);

            foreach (var serverLink in serverLinks.Value)
            {
                var server = await serverService.GetServerByLink(Configuration.BaseUri + serverLink.Href, cancellationToken);

                if (server != null)
                {
                    servers.Add(server);
                }
            }

            return servers;
        }

        /// <summary>
        /// Returns the server Ids for all groups and subgroups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetServerIds()
        {
            return GetServerIds(CancellationToken.None);
        }

        /// <summary>
        /// Returns the server Ids for all groups and subgroups.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IEnumerable<string> GetServerIds(CancellationToken cancellationToken)
        {
            var serverIds = new List<string>();

            if (!HasServers())
            {
                return null;
            }

            return serverLinks.Value.Select(l => l.Id).ToList();
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

            var billingService = new BillingService(Authentication);
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

            var billingService = new BillingService(Authentication);
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

            var dataCenterService = new DataCenterService(Authentication);
            var defaultSettings = await dataCenterService.GetDefaultSettingsByLink(string.Format("{0}{1}", Configuration.BaseUri, defaultsLink.Value.Href), cancellationToken).ConfigureAwait(false);
            return defaultSettings;
        }
    }
}
