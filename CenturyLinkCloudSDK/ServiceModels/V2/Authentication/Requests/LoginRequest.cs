using CenturyLinkCloudSDK.ServiceAPI.Runtime;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Requests
{
    internal class LoginRequest: ServiceRequestModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
