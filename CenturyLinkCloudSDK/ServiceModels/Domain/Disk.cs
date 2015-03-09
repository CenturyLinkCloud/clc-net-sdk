using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class Disk
    {
        public string Id { get; set; }

        public float SizeGB { get; set; }

        public IEnumerable<string> PartitionPaths { get; set; }
    }
}
