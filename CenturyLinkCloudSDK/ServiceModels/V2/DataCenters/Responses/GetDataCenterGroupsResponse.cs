using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.DataCenters.Responses
{
    internal class GetDataCenterGroupsResponse : IServiceResponseModel
    {
        private ServerGroup response = new ServerGroup();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as ServerGroup;
            }
        }
    }
}
