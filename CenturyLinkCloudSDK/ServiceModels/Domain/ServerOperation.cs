using Newtonsoft.Json;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ServerOperation
    {
        public string Server { get; set; }

        public bool IsQueued { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        public string ErrorMessage { get; set; }
    }
}
