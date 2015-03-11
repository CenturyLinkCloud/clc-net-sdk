using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class DataCenter
    {
        private Lazy<Link> computeLimitsLink;

        internal Authentication Authentication { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataCenter()
        {
            computeLimitsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "computeLimits", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public TotalAssets Totals { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Determines if this Group has servers by examining the Links collection.
        /// </summary>
        private bool HasComputeLimits()
        {
            return computeLimitsLink.Value != null ? true : false;
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <returns></returns>
        public async Task<ComputeLimits> GetComputeLimits()
        {
            return await GetComputeLimits(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ComputeLimits> GetComputeLimits(CancellationToken cancellationToken)
        {
            var dataCenterService = new DataCenterService(Authentication);

            var computeLimits = await dataCenterService.GetComputeLimitsByLink(string.Format("{0}{1}", Configuration.BaseUri, computeLimitsLink.Value.Href), cancellationToken).ConfigureAwait(false);

            return computeLimits;
        }
    }
}
