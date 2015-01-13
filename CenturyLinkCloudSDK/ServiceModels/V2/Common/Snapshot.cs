using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class Snapshot
    {
        public string Name { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }
}
