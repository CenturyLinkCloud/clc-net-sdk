using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.Runtime;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class DataCenter
    {
        Lazy<Link> computeLimitsLink;
        Lazy<Link> networkLimitsLink;
        Lazy<Link> billingLink;
        Lazy<Link> rootGroupLink;
        Lazy<Link> defaultsLink;       
        Lazy<Link> createServerLink;        

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataCenter()
        {
            computeLimitsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "computeLimits", StringComparison.CurrentCultureIgnoreCase));
            });

            billingLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "billing", StringComparison.CurrentCultureIgnoreCase));
            });

            rootGroupLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "group", StringComparison.CurrentCultureIgnoreCase));
            });

            defaultsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "defaults", StringComparison.CurrentCultureIgnoreCase));
            });

            networkLimitsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "networkLimits", StringComparison.CurrentCultureIgnoreCase));
            });

            createServerLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "createServer", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        public string Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Whether the total assets were loaded when the data center was fetched
        /// </summary>
        public bool HasTotalAssets { get; internal set; }
        public TotalAssets Totals { get; set; }

        internal GroupService GroupService { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Determines if this data center has compute limits available
        /// </summary>
        public bool HasComputeLimits
        {
            get { return computeLimitsLink.Value != null; }
        }

        /// <summary>
        /// Determines if this data center has billing details available
        /// </summary>
        /// <returns></returns>
        public bool HasBillingDetails
        {
            get { return billingLink.Value != null; }
        }

        /// <summary>
        /// Determines if this data center has a root hardware group available
        /// </summary>
        /// <returns></returns>
        public bool HasRootGroup
        {
            get { return rootGroupLink.Value != null; }
        }

        /// <summary>
        /// Determines if this data center has default settings available
        /// </summary>
        /// <returns></returns>
        public bool HasDefaults
        {
            get { return defaultsLink.Value != null; }
        }

        /// <summary>
        /// Determines if this data center has network limits available
        /// </summary>
        /// <returns></returns>
        public bool HasNetworkLimits
        {
            get { return networkLimitsLink.Value != null; }
        }

        /// <summary>
        /// Determines if the account is authorized to create servers.
        /// </summary>
        /// <returns></returns>
        public bool CanCreateServer
        {
            get { return createServerLink.Value != null; }
        }
        
        /// <summary>
        /// Gets the root group containing all the data center hardware.
        /// </summary>
        /// <returns>The root group</returns>
        public Task<Group> GetRootGroup()
        {
            return GetRootGroup(CancellationToken.None);
        }

        /// <summary>
        /// Gets the root group containing all the data center hardware.
        /// </summary>        
        /// <returns>The root group</returns>
        public Task<Group> GetRootGroup(CancellationToken cancellationToken)
        {
            if (!HasRootGroup)
            {
                return null;
            }

            var uri = string.Format("{0}{1}", Configuration.BaseUri, rootGroupLink.Value.Href);
            return GroupService.GetGroupByLink(uri, cancellationToken);            
        }

        /*
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
            if(!HasBillingDetails())
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
        /// Gets the compute limits.
        /// </summary>
        /// <returns></returns>
        public async Task<ComputeLimits> GetComputeLimits()
        {
            return await GetComputeLimits(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the compute limits.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ComputeLimits> GetComputeLimits(CancellationToken cancellationToken)
        {
            if(!HasComputeLimits())
            {
                return null;
            }

            var dataCenterService = Configuration.ServiceResolver.Resolve<DataCenterService>(Authentication);
            var computeLimits = await dataCenterService.GetComputeLimitsByLink(string.Format("{0}{1}", Configuration.BaseUri, computeLimitsLink.Value.Href), cancellationToken).ConfigureAwait(false);
            return computeLimits;
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
        }

        /// <summary>
        /// Get the network limits.
        /// </summary>
        /// <returns></returns>
        public async Task<NetworkLimits> GetNetworkLimits()
        {
            return await GetNetworkLimits(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the network limits.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<NetworkLimits> GetNetworkLimits(CancellationToken cancellationToken)
        {
            if (!HasNetworkLimits())
            {
                return null;
            }

            var dataCenterService = Configuration.ServiceResolver.Resolve<DataCenterService>(Authentication);
            var networkLimits = await dataCenterService.GetNetworkLimitsByLink(string.Format("{0}{1}", Configuration.BaseUri, networkLimitsLink.Value.Href), cancellationToken).ConfigureAwait(false);
            return networkLimits;
        }*/
    }
}
