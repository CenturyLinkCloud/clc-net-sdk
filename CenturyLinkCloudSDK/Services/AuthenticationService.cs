using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with user authentication.
    /// </summary>
    public class AuthenticationService : ServiceBase
    {
        internal AuthenticationService(IServiceInvoker serviceInvoker)
            : base(null, serviceInvoker)
        {

        }

        /// <summary>
        /// Use this operation before you call any other API operation. It authenticates a user and  
        /// returns the account alias a user's roles, the primary data center, and a valid bearer token. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserInfo> Login(string username, string password)
        {
            return await Login(username, password, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Use this operation before you call any other API operation. It authenticates a user and  
        /// returns the account alias a user's roles, the primary data center, and a valid bearer token. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserInfo> Login(string username, string password, CancellationToken cancellationToken)
        {
            var requestModel = new LoginRequest() { UserName = username, Password = password };

            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Authentication.Login, Configuration.BaseUri), requestModel);
            var result = await serviceInvoker.Invoke<UserInfo>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
