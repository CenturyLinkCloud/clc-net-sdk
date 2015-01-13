using System;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using System.Net.Http;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Requests;
using CenturyLinkCloudSDK.ServiceModels.V2.Servers.Requests;


namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Authentication : ServiceAPIBase
    {
        public async Task<LoginResponse> Login(string username, string password)
        {
            var loginRequest = new LoginRequest(){ UserName = username, Password = password };

            //var req = new GetServerRequest() { Parameters = new GetServerRequest.UriParameters { AccountAlias = "", ServerId = "" } };

            var request = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = "v2/authentication/login",
                MediaType = "application/json",
                RequestModel = loginRequest,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, LoginResponse>(request);

            return result;

        }
    }
}
