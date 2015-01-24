using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.DataCenters.Responses
{
    /// <summary>
    /// This class contains the response from the GetDataCenters operation.
    /// </summary>
    internal class GetDataCentersResponse: IServiceResponse
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
