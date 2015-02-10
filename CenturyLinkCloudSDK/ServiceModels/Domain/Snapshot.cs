using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class Snapshot
    {
        public string Name { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }
}
