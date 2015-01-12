using System;

namespace CenturyLinkCloudSDK.ServiceModels.V2
{
    public class Credentials: IServiceModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
