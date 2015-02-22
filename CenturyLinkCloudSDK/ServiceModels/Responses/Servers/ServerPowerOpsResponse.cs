using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.Responses.Servers
{
    /// <summary>
    /// This class contains the response from the GetServer operation.
    /// </summary>
    internal class ServerPowerOpsResponse : IResponseRoot<IEnumerable<ServerOperation>>
    {
        public IEnumerable<ServerOperation> Response { get; set; }
    }
}
