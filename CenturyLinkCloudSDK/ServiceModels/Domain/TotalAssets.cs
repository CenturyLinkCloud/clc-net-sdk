using System;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class TotalAssets
    {
        public int Servers { get; set; }

        public int Cpus { get; set; }

        public int MemoryGB { get; set; }

        public int StorageGB { get; set; }

        public int Queue { get; set; }

        public string MemoryGBFormatted { get; set; }

        public string StorageGBFormatted { get; set; }
    }
}
