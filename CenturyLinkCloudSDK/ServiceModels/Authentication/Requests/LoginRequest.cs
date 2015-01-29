using CenturyLinkCloudSDK.Services.Runtime;

namespace CenturyLinkCloudSDK.ServiceModels.Authentication.Requests
{
    /// <summary>
    /// This class contains the user credentials required during the Login operation.
    /// </summary>
    internal class LoginRequest: ServiceRequestModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
