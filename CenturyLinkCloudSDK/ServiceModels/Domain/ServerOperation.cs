using Newtonsoft.Json;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class ServerOperation
    {
        public string Server { get; set; }

        public bool IsQueued { get; set; }

        [JsonPropertyAttribute]
        private IReadOnlyList<Link> Links { get; set; }

        public string ErrorMessage { get; set; }
    }
}
