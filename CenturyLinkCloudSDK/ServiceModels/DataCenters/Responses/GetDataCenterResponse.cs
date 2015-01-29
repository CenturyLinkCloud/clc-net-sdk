using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.DataCenters.Responses
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
