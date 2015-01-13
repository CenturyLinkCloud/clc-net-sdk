using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class PortDetail
    {
        public string Protocol { get; set; }

        public int Port { get; set; }

        public int PortTo { get; set; }
    }
}
