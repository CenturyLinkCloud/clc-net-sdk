using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Collections.Generic;


namespace CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses
{
    public class ServerPowerOpsResponse: IServiceResponseModel
    {
        public List<ServerOperation> Response { get; set; }
    }
}
