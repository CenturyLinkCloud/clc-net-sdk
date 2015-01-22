using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Servers.Responses
{
    internal class GetServerResponse: IServiceResponse
    {
        private Server response = new Server();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as Server;
            }
        }

    }
}
