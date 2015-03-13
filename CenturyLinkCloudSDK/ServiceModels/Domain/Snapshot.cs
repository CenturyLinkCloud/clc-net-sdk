using Newtonsoft.Json;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Snapshot
    {
        public string Name { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }
    }
}
