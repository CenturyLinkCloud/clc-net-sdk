using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Responses.DataCenters
{
    /// <summary>
    /// This class contains the response from the GetDataCenter operation.
    /// </summary>
    internal class GetDataCenterResponse : IResponseRoot<DataCenter>
    {
        public DataCenter Response { get; set; }
    }
}
