using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.Requests.Server
{
    public abstract class CreateServerRequest
    {
        public CreateServerRequest(string type, string name, string parentGroupId)
        {
            Type = type;
            Name = name;
            GroupId = parentGroupId;
        }

        public string Name { get; private set; }
        public string GroupId { get; private set; }
        public string Type { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryDns { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryDns { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NetworkId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CustomField> CustomFields { get; set; }
    }

    public class CreateBareMetalServerRequest : CreateServerRequest
    {
        public CreateBareMetalServerRequest(string name, string parentGroupId, string configurationId, string osType)
            : base("bareMetal", name, parentGroupId)
        {
            ConfigurationId = configurationId;
            OsType = osType;
        }

        public string ConfigurationId { get; private set; }
        public string OsType { get; private set; }
    }

    public abstract class CreateNonBareMetalServerRequest : CreateServerRequest
    {
        public CreateNonBareMetalServerRequest(string type, string name, string parentGroupId, string sourceServerId, int cpuCount, int memoryGb)
            : base(type, name, parentGroupId)
        {
            SourceServerId = sourceServerId;

            if ((cpuCount < 1) || (cpuCount > 16)) throw new ArgumentOutOfRangeException("cpuCount", "cpuCount must be between 1 and 16");
            Cpu = cpuCount;

            if ((memoryGb < 1) || (memoryGb > 128)) throw new ArgumentOutOfRangeException("memoryGb", "memoryGb must be between 1 and 128");
            MemoryGB = memoryGb;
        }
        
        public string SourceServerId { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SourceServerPassword { get; set; }

        public int Cpu { get; private set; }
        public int MemoryGB { get; private set; }

        public bool IsManagedOs { get; set; }
        public bool IsManagedBackup { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IpAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CpuAutoscalePolicyId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StorageType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<AdditionalDisk> AdditionalDisks { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Package> Packages { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Ttl { get; set; }

    }

    public class CreateStandardServerRequest : CreateNonBareMetalServerRequest
    {
        public CreateStandardServerRequest(string name, string parentGroupId, string sourceServerId, int cpuCount, int memoryGb) 
            : base("standard", name, parentGroupId, sourceServerId, cpuCount, memoryGb)
        {
        }
    }

    public class CreateHyperscaleServerRequest : CreateNonBareMetalServerRequest
    {
        public CreateHyperscaleServerRequest(string name, string parentGroupId, string sourceServerId, int cpuCount, int memoryGb) 
            : base("hyperscale", name, parentGroupId, sourceServerId, cpuCount, memoryGb)
        {
            StorageType = "hyperscale";
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AntiAffinityPolicyId { get; set; }
    }
}
