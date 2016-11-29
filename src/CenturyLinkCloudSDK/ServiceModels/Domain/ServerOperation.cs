using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ServerOperation
    {
        private Lazy<Link> queueStatusLink;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ServerOperation()
        {
            queueStatusLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "status", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        public string Server { get; set; }

        public bool IsQueued { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        public string ErrorMessage { get; set; }

        public Link QueueStatusLink
        {
            get { return queueStatusLink.Value; }
        }

        public string QueueStatusId
        {
            get 
            {
                return QueueStatusLink.Id;           
            }
        }
    }
}
