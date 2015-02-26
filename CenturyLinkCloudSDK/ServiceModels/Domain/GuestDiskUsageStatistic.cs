using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GuestDiskUsageStatistic
    {
        public string Path { get; set; }

        public long CapacityMB { get; set; }

        public long ConsumedMB { get; set; }
    }
}
