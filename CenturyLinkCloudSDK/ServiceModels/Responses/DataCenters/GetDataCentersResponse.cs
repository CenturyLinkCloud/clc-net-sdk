using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.Responses.DataCenters
{
    /// <summary>
    /// This class contains the response from the GetDataCenters operation.
    /// </summary>
    internal class GetDataCentersResponse : IResponseRoot<IReadOnlyList<DataCenter>>
    {
        public IReadOnlyList<DataCenter> Response { get; set; }
    }
}
