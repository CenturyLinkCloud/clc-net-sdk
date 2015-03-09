using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
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

        public IEnumerable<Disk> Disks { get; set; }

        public IEnumerable<Partition> Partitions { get; set; }

        public IEnumerable<Snapshot> Snapshots { get; set; }

        public IEnumerable<CustomField> CustomFields { get; set; }
    }
}
