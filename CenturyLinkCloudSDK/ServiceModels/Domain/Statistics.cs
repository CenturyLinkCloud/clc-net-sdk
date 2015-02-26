using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels 
{
    public class Statistics
    {
        public string Name { get; set; }

        public IEnumerable<ServerStatistic> Stats { get; set; }
    }
}
