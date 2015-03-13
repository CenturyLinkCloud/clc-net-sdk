using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Server
    {
        private Lazy<Link> statisticsLink;

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        public Authentication Authentication { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Server()
        {
            statisticsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "statistics", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GroupId { get; set; }

        public bool IsTemplate { get; set; }

        public string LocationId { get; set; }

        public string OsType { get; set; }

        public string Status { get; set; }

        public ServerDetail Details { get; set; }

        public string Type { get; set; }

        public string StorageType { get; set; }

        public ChangeInfo ChangeInfo { get; set; }

        /// <summary>
        /// Determines if this Group has servers by examining the Links collection.
        /// </summary>
        private bool HasStatistics()
        {
            return statisticsLink.Value != null ? true : false;
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <returns></returns>
        public async Task<Statistics> GetStatistics()
        {
            return await GetStatistics(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Statistics> GetStatistics(CancellationToken cancellationToken)
        {
            var servers = new List<Server>();
            var serverService = new ServerService(Authentication);

            var statistics = await serverService.GetServerStatisticsByLink(string.Format("{0}{1}{2}", Configuration.BaseUri, statisticsLink.Value.Href, Constants.ServiceUris.Querystring.GetLatestStatistics), cancellationToken).ConfigureAwait(false);

            return statistics;
        }
    }
}
