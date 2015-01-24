using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.DataCenters.Responses
{
    /// <summary>
    /// This class contains the response from the GetDataCenterGroups operation.
    /// </summary>
    internal class GetDataCenterGroupsResponse : IServiceResponse
    {
        private DataCenterGroup response = new DataCenterGroup();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as DataCenterGroup;
            }
        }
    }
}
