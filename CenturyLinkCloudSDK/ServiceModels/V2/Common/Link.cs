using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class Link
    {
        public string Rel { get; set; }

        public string Href { get; set; }

        public string[] Verbs { get; set; }

        public string Id { get; set; }
    }
}
