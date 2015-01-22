using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Servers.Responses
{
    internal class ServerPowerOpsResponse: IServiceResponseModel
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
