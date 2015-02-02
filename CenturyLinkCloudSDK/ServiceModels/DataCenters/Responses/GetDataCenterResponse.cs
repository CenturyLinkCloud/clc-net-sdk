using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.DataCenters.Responses
{
    /// <summary>
    /// This class contains the response from the GetDataCenter operation.
    /// </summary>
    public class GetDataCenterResponse : IResponseRoot<DataCenter>
    {
        public DataCenter Response { get; set; }
    }
}
