using System;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GroupBillingDetail
    {
        public DateTime Date { get; set; }

        public IEnumerable<GroupBilling> Groups { get; set; }
    }
}
