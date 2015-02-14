using CenturyLinkCloudSDK.Runtime;

namespace CenturyLinkCloudSDK.ServiceModels.Requests.Authentication
{
    /// <summary>
    /// This class contains the user credentials required during the Login operation.
    /// </summary>
    internal class LoginRequest: ServiceRequestModelBase
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
