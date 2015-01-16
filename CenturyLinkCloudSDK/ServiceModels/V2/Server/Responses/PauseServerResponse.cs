using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses
{
    public class PauseServerResponse: IServiceResponseModel
    {
        public List<ServerOperationResponse> Response { get; set; }
    }
}
