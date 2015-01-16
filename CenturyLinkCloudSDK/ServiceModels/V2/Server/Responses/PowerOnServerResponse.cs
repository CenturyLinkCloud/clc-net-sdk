using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses
{
    public class PowerOnServerResponse : IServiceResponseModel
    {
        public string Server { get; set; }

        public bool IsQueued { get; set; }

        public List<Link> Links { get; set; }

        public string ErrorMessage { get; set; }
    }
}
