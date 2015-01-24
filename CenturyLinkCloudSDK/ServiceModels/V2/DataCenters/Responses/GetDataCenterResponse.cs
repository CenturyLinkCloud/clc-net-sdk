using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.DataCenters.Responses
{
    /// <summary>
    /// This class contains the response from the GetDataCenter operation.
    /// </summary>
    public class GetDataCenterResponse: IServiceResponse
    {
        private DataCenter response = new DataCenter();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as DataCenter;
            }
        }
    }
}
