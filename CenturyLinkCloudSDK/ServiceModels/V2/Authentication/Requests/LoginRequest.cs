using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Requests
{
    public class LoginRequest: IServiceRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
