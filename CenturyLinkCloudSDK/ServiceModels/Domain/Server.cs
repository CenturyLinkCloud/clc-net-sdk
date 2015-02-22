using Newtonsoft.Json;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class Server
    {
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

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }
    }
}
