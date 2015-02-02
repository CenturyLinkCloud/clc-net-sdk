using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Servers.Responses
{
    /// <summary>
    /// This class contains the response from the GetServer operation.
    /// </summary>
    internal class GetServerResponse : IResponseRoot<Server>
    {
        public Server Response { get; set; }
    }
}
