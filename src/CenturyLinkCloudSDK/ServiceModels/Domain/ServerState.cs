using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ServerState
    {
        public string ServerId { get; set; }

        public string ServerName { get; set; }

        public bool InMaintenanceMode { get; set; }

        public string PowerState { get; set; }

        public bool Selected { get; set; }
    }
}
