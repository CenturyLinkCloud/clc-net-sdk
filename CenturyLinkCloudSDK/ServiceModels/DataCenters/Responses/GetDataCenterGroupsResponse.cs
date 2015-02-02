using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.DataCenters.Responses
{
    /// <summary>
    /// This class contains the response from the GetDataCenterGroups operation.
    /// </summary>
    internal class GetDataCenterGroupsResponse : IResponseRoot<DataCenterGroup>
    {
        public DataCenterGroup Response { get; set; }
    }
}
