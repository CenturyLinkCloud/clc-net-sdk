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

        private Lazy<Link> selfLink;

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

            selfLink = new Lazy<Link>(() =>
            {
                return Links.Where(l => String.Equals(l.Rel, "self", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
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
                var serverService = Configuration.ServiceResolver.Resolve<ServerService>(Authentication);

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

        /// <summary>
        /// Gets the triggers. If the Triggers property is null (coming from the GetServer method that is the case).
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AlertTrigger>> GetTriggers()
        {
            return await GetTriggers(CancellationToken.None);
        }

        /// <summary>
        ///  /// Gets the triggers. If the Triggers property is null (coming from the GetServer method that is the case).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AlertTrigger>> GetTriggers(CancellationToken cancellationToken)
        {
            if (Triggers == null)
            {
                if (selfLink.Value != null)
                {
                    var alertService = Configuration.ServiceResolver.Resolve<AlertService>(Authentication);
                    return await alertService.GetTriggersByAlertPolicyLink(Configuration.BaseUri + selfLink.Value.Href, cancellationToken);
                }

                return null;
            }
            else
            {
                return Triggers;
            } 
        }
    }
}
