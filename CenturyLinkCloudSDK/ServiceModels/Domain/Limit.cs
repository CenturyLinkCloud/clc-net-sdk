namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class Limit
    {
        public int Cpu { get; set; }

        public int MemoryGB { get; set; }

        public int StorageGB { get; set; }
    }
}
