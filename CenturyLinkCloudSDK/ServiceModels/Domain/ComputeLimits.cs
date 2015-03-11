namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class ComputeLimits
    {
        public Limit Cpu { get; set; }

        public Limit MemoryGB { get; set; }

        public Limit StorageGB { get; set; }
    }
}
