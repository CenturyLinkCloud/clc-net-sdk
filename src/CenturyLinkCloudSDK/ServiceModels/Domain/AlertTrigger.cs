using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class AlertTrigger
    {
        public string Metric { get; set; }

        public TimeSpan Duration { get; set; }

        public Int16 Threshold { get; set; }
    }
}
