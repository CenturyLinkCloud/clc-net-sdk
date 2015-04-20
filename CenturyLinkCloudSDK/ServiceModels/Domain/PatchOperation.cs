using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class PatchOperation
    {
        public string Op { get; set; }

        public string Member { get; set; }

        public int Value { get; set; }
    }
}
