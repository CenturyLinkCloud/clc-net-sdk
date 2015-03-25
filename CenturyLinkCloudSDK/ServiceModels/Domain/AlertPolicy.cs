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
    public class AlertPolicy
    {
        private Lazy<IEnumerable<Link>> serverLinks;

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        public Authentication Authentication { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AlertPolicy()
        {
            serverLinks = new Lazy<IEnumerable<Link>>(()=>
            {
                return Links.Where(l => String.Equals(l.Rel, "server", StringComparison.CurrentCultureIgnoreCase)).ToList();
            });
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AlertAction> Actions { get; set; }

        public IEnumerable<AlertTrigger> Triggers { get; set; }

        /// <summary>
        /// Determines if this Group has servers by examining the Links collection.
        /// </summary>
        private bool HasServers()
        {
            return serverLinks.Value.Count() > 0 ? true : false;
        }

        /// <summary>
        /// Gets the servers that are subscribed to this alert policy.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Server>> GetServers()
        {
            return await GetServers(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the servers that are subscribed to this alert policy.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Server>> GetServers(CancellationToken cancellationToken)
        {
            var servers = new List<Server>();

            if (HasServers())
            {
                var serverService = new ServerService(Authentication);

                foreach (var serverLink in serverLinks.Value)
                {
                    var server = await serverService.GetServerByLink(Configuration.BaseUri + serverLink.Href, cancellationToken);

                    if (server != null)
                    {
                        servers.Add(server);
                    }
                }
            }

            return servers;
        }
    }
}
