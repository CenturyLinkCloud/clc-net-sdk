using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ServerPricing
    {
        public float Cpu { get; set; }

        public float MemoryGB { get; set; }

        public float StorageGB { get; set; }

        public float ManagedOS { get; set; }
    }
}
