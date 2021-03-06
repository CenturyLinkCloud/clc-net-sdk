﻿using CenturyLinkCloudSDK.Runtime;
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
        internal Lazy<Link> selfLink;

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        internal ServerService ServerService { get; set; }

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
        /// Determines if this alert policy is applied to servers
        /// </summary>
        private bool HasServers
        {
            get { return serverLinks.Value.Count() > 0; }
        }

        /// <summary>
        /// Gets the servers that are subscribed to this alert policy.
        /// </summary>
        /// <returns>The servers for this policy</returns>
        public Task<IEnumerable<Server>> GetServers()
        {
            return GetServers(CancellationToken.None);
        }

        /// <summary>
        /// Gets the servers that are subscribed to this alert policy.
        /// </summary>
        /// <returns>The servers for this policy</returns>
        public Task<IEnumerable<Server>> GetServers(CancellationToken cancellationToken)
        {
            return
                ServerService
                    .GetServers(
                        HasServers ?
                            serverLinks.Value.Select(l => l.Id) :
                            Enumerable.Empty<string>(),
                        cancellationToken);
        }        
    }
}
