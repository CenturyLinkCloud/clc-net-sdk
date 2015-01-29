using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Servers.Responses
{
    /// <summary>
    /// This class contains the response from the GetServer operation.
    /// </summary>
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
