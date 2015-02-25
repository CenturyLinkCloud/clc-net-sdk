using System;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GroupBillingDetail
    {
        public DateTime Date { get; set; }

        //public IEnumerable<GroupBilling> Groups { get; set; }

        public Dictionary<string, GroupBilling> Groups { get; set; }
    }
}
