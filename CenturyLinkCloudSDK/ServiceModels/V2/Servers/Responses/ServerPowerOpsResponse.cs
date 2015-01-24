using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Servers.Responses
{
    /// <summary>
    /// This class contains the response from the GetServer operation.
    /// </summary>
    internal class ServerPowerOpsResponse: IServiceResponse
    {
        private IEnumerable<ServerOperation> response = new List<ServerOperation>();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as IEnumerable<ServerOperation>;
            }
        }
    }
}
