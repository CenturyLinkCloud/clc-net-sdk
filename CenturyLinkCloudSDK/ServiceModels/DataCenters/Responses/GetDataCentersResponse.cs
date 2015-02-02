using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.DataCenters.Responses
{
    /// <summary>
    /// This class contains the response from the GetDataCenters operation.
    /// </summary>
    internal class GetDataCentersResponse : IResponseRoot<IEnumerable<DataCenter>>
    {
        public IEnumerable<DataCenter> Response { get; set; }
    }
}
