using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses
{
    public class LoginResponse: IServiceResponseModel
    {
        public string UserName { get; set; }

        public string AccountAlias { get; set; }

        public string LocationAlias { get; set; }

        public string[] Roles { get; set; }

        public string BearerToken { get; set; }
    }
}
