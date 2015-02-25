using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class AlertPolicyItems
    {
        public IEnumerable<AlertPolicy> Items { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }
    }
}
