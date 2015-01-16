using System;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using System.Net.Http;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Requests;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Authentication : ServiceAPIBase
    {
        public async Task<LoginResponse> Login(string username, string password)
        {
            var requestModel = new LoginRequest(){ UserName = username, Password = password };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = "v2/authentication/login",
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, LoginResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                Persistence.UserInfo = new LoginResponse();
                Persistence.UserInfo.BearerToken = result.BearerToken;
                Persistence.UserInfo.AccountAlias = result.AccountAlias;
                Persistence.UserInfo.Roles = result.Roles;
            }

            return result;

        }
    }
}
