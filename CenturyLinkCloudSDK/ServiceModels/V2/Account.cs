using System;

namespace CenturyLinkCloudSDK.ServiceModels.V2
{
    public class Account: IServiceModel
    {
        public string UserName { get; set; }
        public string AccountAlias { get; set; }
        public string LocationAlias { get; set; }
        public string[] Roles { get; set; }
        public string BearerToken { get; set; }
    }
}
