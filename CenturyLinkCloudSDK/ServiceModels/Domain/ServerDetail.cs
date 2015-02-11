using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class ServerDetail
    {
        public IReadOnlyList<IpAddress> IpAddresses { get; set; }

        public IReadOnlyList<AlertPolicy> AlertPolicies { get; set; }

        public int Cpu { get; set; }

        public int DiskCount { get; set; }

        public string HostName { get; set; }

        public bool InMaintenanceMode { get; set; }

        public int MemoryMB { get; set; }

        public string PowerState { get; set; }

        public int StorageGB { get; set; }

        public IReadOnlyList<Snapshot> Snapshots { get; set; }

        public IReadOnlyList<CustomField> CustomFields { get; set; }
    }
}
