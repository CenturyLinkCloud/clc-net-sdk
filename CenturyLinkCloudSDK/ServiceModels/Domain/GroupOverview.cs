using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GroupOverview
    {
        public Group Group { get; set; }

        public BillingDetail BillingTotals { get; set; }

        public TotalAssets TotalAssets { get; set; }

        public DefaultSettings DefaultSettings { get; set; }

        public IEnumerable<Activity> RecentActivity { get; set; }
    }
}
