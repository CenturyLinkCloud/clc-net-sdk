using System;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Authentication : ServiceAPIBase
    {
        public async Task<Account> Login(string username, string password)
        {

            var credentials = new Credentials(){ UserName = username, Password = password };

            var request = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = "v2/authentication/login",
                MediaType = "application/json",
                RequestModel = credentials,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, Account>(request);

            return result;

        }
    }
}
