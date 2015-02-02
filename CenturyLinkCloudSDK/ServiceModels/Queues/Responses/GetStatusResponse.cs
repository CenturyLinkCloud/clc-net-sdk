using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Queues.Responses
{
    /// <summary>
    /// This class contains the response from the GetStatus operation.
    /// </summary>
    internal class GetStatusResponse : IResponseRoot<Queue>
    {
        public Queue Response { get; set; }
    }
}
