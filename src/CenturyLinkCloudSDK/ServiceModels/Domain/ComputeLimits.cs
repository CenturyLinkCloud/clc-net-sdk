namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ComputeLimits
    {
        public IntegerLimit Cpu { get; set; }

        public IntegerLimit MemoryGB { get; set; }

        public IntegerLimit StorageGB { get; set; }

        public string MemoryGBFormatted { get; set; }

        public string StorageGBFormatted { get; set; }
    }
}
