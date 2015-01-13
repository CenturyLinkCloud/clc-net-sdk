using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class ServerDetail
    {
        public IEnumerable<IpAddress> IpAddresses { get; set; }

        public IEnumerable<AlertPolicy> AlertPolicies { get; set; }

        public int Cpu { get; set; }

        public int DiskCount { get; set; }

        public string HostName { get; set; }

        public bool InMaintenanceMode { get; set; }

        public int MemoryMB { get; set; }

        public string PowerState { get; set; }

        public int StorageGB { get; set; }

        public IEnumerable<Snapshot> Snapshots { get; set; }

        public IEnumerable<CustomField> CustomFields { get; set; }
    }
}
