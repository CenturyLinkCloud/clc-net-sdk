using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GroupBilling
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ServerBilling> Servers { get; set; }
    }
}
