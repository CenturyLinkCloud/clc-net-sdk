using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Template
    {
        public string Name { get; set; }

        public string OsType { get; set; }

        public string Description { get; set; }

        public int StorageSizeGB { get; set; }

        public IEnumerable<string> Capabilities { get; set; }

        public IEnumerable<string> ReservedDrivePaths { get; set; }

        public int DrivePathLength { get; set; }
    }
}
