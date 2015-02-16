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
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class Group
    {
        internal Authentication Authentication { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public int ServerCount { get; set; }

        public Limit Limits { get; set; }

        public IReadOnlyList<Group> Groups { get; set; }

        private IReadOnlyList<Link> ServerLinks { get; set; }

        [JsonPropertyAttribute]
        private IReadOnlyList<Link> Links { get; set; }

        /// <summary>
        /// Determines if this Group has servers by examining the Links collection.
        /// </summary>
        public bool HasServers
        {
            get
            {
                if (Links != null)
                {
                    var hasServers = Links.Any(l => l.Rel.ToUpper() == "SERVER");

                    if (hasServers)
                    {
                        ServerLinks = Links.Where(l => l.Rel.ToUpper() == "SERVER").ToList();

                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<Server>> GetServers()
        {
            return await GetServers(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the servers that belong to this group.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Server>> GetServers(CancellationToken cancellationToken)
        {
            var servers = new List<Server>();

            if (HasServers)
            {
                var serverService = new ServerService(Authentication);

                foreach (var serverLink in ServerLinks)
                {
                    var server = await serverService.GetServerByLink(Configuration.BaseUri + serverLink.Href, cancellationToken);
                    servers.Add(server);
                }
            }

            return servers;
        }
    }
}
