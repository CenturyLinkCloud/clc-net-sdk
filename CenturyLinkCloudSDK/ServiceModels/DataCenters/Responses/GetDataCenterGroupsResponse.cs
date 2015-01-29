using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.DataCenters.Responses
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
