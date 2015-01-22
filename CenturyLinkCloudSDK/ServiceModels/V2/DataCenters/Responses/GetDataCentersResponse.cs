using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.DataCenters.Responses
{
    internal class GetDataCentersResponse: IServiceResponseModel
    {
        private IEnumerable<DataCenter> response = new List<DataCenter>();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as IEnumerable<DataCenter>;
            }
        }
    }
}
