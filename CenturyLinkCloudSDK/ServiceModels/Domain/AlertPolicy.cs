using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class AlertPolicy
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyList<Link> Links { get; set; }
    }
}
