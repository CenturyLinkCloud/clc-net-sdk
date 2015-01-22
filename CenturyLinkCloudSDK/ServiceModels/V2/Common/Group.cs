using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class Group
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public int ServerCount { get; set; }

        public Limit Limits { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }
}
