using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Server.Requests
{
    public class UpdatePublicIpAddressRequest: IServiceRequestModel
    {
        //public UriParameters Parameters { get; set; }

        public IEnumerable<PortDetail> Ports { get; set; }

        public IEnumerable<SourceRestriction> SourceRestrictions { get; set; }

        //public class UriParameters
        //{
        //    public string AccountAlias { get; set; }

        //    public string ServerId { get; set; }

        //    public string PublicIp { get; set; }
        //}
       
    }
}
