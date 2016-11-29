using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class DataCenterOverview
    {
        public DataCenter DataCenter { get; set; }
        public ComputeLimits ComputeLimits { get; set; }
        public BillingDetail BillingTotals { get; set; }
        public DefaultSettings DefaultSettings { get; set; }
        public NetworkLimits NetworkLimits { get; set; }

        public IEnumerable<Activity> RecentActivity { get; set; }
    }
}
