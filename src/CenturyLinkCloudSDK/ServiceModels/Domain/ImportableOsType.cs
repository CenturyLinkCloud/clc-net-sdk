using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ImportableOsType
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string LabProductCode { get; set; }

        public string PremiumProductCode { get; set; }

        public string Type { get; set; }
    }
}
