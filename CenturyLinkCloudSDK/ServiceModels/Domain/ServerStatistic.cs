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

        public decimal CpuPercent { get; set; }

        public int MemoryMB { get; set; }

        public decimal MemoryPercent { get; set; }

        public long NetworkReceivedKBps { get; set; }

        public long NetworkTransmittedKBps { get; set; }

        public long DiskUsageTotalCapacityMB { get; set; }

        public IEnumerable<DiskUsageStatistic> DiskUsage { get; set; }

        public IEnumerable<GuestDiskUsageStatistic> GuestDiskUsage { get; set; }
    }
}
