using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Requests;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    /// <summary>
    /// This class contains operations associated with user authentication.
    /// </summary>
    public class AuthenticationService : ServiceBase
    {
        /// <summary>
        /// Use this operation before you call any other API operation. It authenticates a user and  
        /// returns the account alias a user's roles, the primary data center, and a valid bearer token.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>An asynchronous Task of UserInfo object.</returns>
        public async Task<UserInfo> Login(string username, string password)
        {
            var requestModel = new LoginRequest(){ UserName = username, Password = password };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = "https://api.tier3.com/v2/authentication/login",
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, LoginResponse>(serviceRequest).ConfigureAwait(false);

            if (result == null)
            {
                Authentication.UserInfo = null;
                return null;
            }

            var response = result.Response as UserInfo;

            if (response.BearerToken == null)
            {
                Authentication.UserInfo = null;
                return null;
            }

            Authentication.UserInfo = new UserInfo();
            Authentication.UserInfo.BearerToken = response.BearerToken;
            Authentication.UserInfo.AccountAlias = response.AccountAlias;
            Authentication.UserInfo.Roles = response.Roles;

            return response;
        }
    }
}
