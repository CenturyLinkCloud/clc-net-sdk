using System;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses
{
    public class UpdatePublicIpAddressResponse: IServiceResponseModel
    {
        public string Rel { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }
    }
}
