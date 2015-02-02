using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Authentication.Responses
{
    /// <summary>
    /// This class contains the response from the Login operation.
    /// </summary>
    internal class LoginResponse : IResponseRoot<UserInfo>
    {
        public UserInfo Response { get; set; }
    }
}
