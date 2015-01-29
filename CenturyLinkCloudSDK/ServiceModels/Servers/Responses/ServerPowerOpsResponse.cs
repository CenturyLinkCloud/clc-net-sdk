using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.Servers.Responses
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
