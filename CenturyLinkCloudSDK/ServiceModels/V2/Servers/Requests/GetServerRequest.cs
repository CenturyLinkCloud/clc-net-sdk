
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Servers.Requests
{
    public class GetServerRequest: IServiceRequestModel
    {
        public UriParameters Parameters { get; set; }

        public class UriParameters
        {
            public string AccountAlias { get; set; }

            public string ServerId { get; set; }
        }
    }
}
