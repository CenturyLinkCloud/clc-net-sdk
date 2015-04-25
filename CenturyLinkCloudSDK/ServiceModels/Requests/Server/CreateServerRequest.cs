using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.Requests.Server
{
    public class CreateServerRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string GroupId { get; set; }

        public string SourceServerId { get; set; }

        public bool IsManagedOs { get; set; }

        public string PrimaryDns { get; set; }

        public string SecondaryDns { get; set; }

        public string NetworkId { get; set; }

        public string IpAddress { get; set; }

        public string Password { get; set; }

        public string SourceServerPassword { get; set; }

        public int Cpu { get; set; }

        public string CpuAutoscalePolicyId { get; set; }

        public int MemoryGB { get; set; }

        public string Type { get; set; }

        public string StorageType { get; set; }

        public string AntiAffinityPolicyId { get; set; }

        public IEnumerable<CustomField> CustomFields { get; set; }

        public IEnumerable<AdditionalDisk> AdditionalDisks { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Ttl { get; set; }

        public IEnumerable<Package> Packages { get; set; }
    }
}
