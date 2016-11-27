using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ServerStatistic
    {
        public DateTime Timestamp { get; set; }

        public short Cpu { get; set; }

        public float CpuPercent { get; set; }

        public float MemoryMB { get; set; }

        public float MemoryPercent { get; set; }

        public float NetworkReceivedKBps { get; set; }

        public float NetworkTransmittedKBps { get; set; }

        public float DiskUsageTotalCapacityMB { get; set; }

        public IEnumerable<DiskUsageStatistic> DiskUsage { get; set; }

        public IEnumerable<GuestDiskUsageStatistic> GuestDiskUsage { get; set; }
    }
}
