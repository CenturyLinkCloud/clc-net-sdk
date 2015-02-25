using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GroupBilling
    {
        public string Name { get; set; }

        public Dictionary<string, ServerBilling> Servers { get; set; }
    }
}
