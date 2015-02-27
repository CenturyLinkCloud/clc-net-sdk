using System;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class TotalAssets
    {
        public Int16 Servers { get; set; }

        public Int16 Cpus { get; set; }

        public Int16 MemoryGB { get; set; }

        public Int16 StorageGB { get; set; }

        public Int16 Queue { get; set; }
    }
}
