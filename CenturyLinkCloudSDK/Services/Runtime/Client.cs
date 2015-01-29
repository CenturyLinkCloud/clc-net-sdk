using CenturyLinkCloudSDK.ServiceModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services.Runtime
{
    /// <summary>
    /// This is the facade that provides access to all API services.
    /// </summary>
    public class Client
    {
        private UserInfo userInfo = null;
        private AuthenticationInfo authenticationInfo = null;

        /// <summary>
        /// Constructor called when the user needs to be authenticated. It sets the userInfo and authenticationInfo fields
        /// that can then be accessed by the corresponding readonly properties. The UserInfo for User Info display and the AuthenticationInfo
        /// for subsequent calls to the API after the authentication has succeeded.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public Client(string userName, string password)
        {
            userInfo = AuthenticateUser(userName, password).Result;
            authenticationInfo = new AuthenticationInfo() { AccountAlias = userInfo.AccountAlias, BearerToken = userInfo.BearerToken };
        }

        /// <summary>
        /// Constructor called after an authentication request has succeeded. The AuthenticationInfo object
        /// contains properties for Account Alias and BearerToken.
        /// </summary>
        /// <param name="authenticationInfo"></param>
        public Client(AuthenticationInfo authenticationInfo)
        {
            this.authenticationInfo = authenticationInfo;
        }

        public UserInfo UserInfo 
        { 
            get
            {
                return userInfo;
            }
        }

        public AuthenticationInfo AuthenticationInfo
        {
            get
            {
                return authenticationInfo;
            }
        }

        public AuthenticationService AuthenticationService
        {
            get
            {
                return new AuthenticationService();
            }
        }

        public DataCenterService DataCenterService
        {
            get
            {
                return new DataCenterService(authenticationInfo);
            }
        }

        public GroupService GroupService
        {
            get
            {
                return new GroupService(authenticationInfo);
            }
        }

        public QueueService QueueService
        {
            get
            {
                return new QueueService(authenticationInfo);
            }
        }

        public ServerService ServerService
        {
            get
            {
                return new ServerService(authenticationInfo);
            }
        }

        private async Task<UserInfo> AuthenticateUser(string userName, string password)
        {
            return await AuthenticationService.Login(userName, password).ConfigureAwait(false);
        }
    }
}
