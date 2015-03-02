using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Alert
    {
        public string ServerId { get; set; }

        public string AlertPolicyId { get; set; }

        public string Metric { get; set; }

        public string Message { get; set; }

        public DateTime GeneratedOn { get; set; }
    }
}
