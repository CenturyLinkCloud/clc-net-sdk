using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Groups.Responses
{
    /// <summary>
    /// This class contains the response from the GetGroup operation.
    /// </summary>
    internal class GetGroupResponse : IResponseRoot<Group>
    {
        public Group Response { get; set; }
    }
}
