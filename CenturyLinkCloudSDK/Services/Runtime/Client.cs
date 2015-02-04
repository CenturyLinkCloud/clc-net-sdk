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
        private bool userIsAuthenticated = false;
        private AuthenticationService authentication;
        private DataCenterService dataCenters;
        private GroupService groups;
        private QueueService queues;
        private ServerService servers;

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

            if (userInfo != null)
            {
                userIsAuthenticated = true;
                authenticationInfo = new AuthenticationInfo() { AccountAlias = userInfo.AccountAlias, BearerToken = userInfo.BearerToken };
                InitializeServices();
            }
            else
            {
                userIsAuthenticated = false;
            }
        }

        /// <summary>
        /// Constructor called after an authentication request has succeeded. The AuthenticationInfo object
        /// contains properties for Account Alias and BearerToken.
        /// </summary>
        /// <param name="authenticationInfo"></param>
        public Client(AuthenticationInfo authenticationInfo)
        {
            if (authenticationInfo != null)
            {
                userIsAuthenticated = true;
                this.authenticationInfo = authenticationInfo;
                InitializeServices();
            }
            else
            {
                userIsAuthenticated = false;
            }
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

        public AuthenticationService Authentication
        {
            get
            {
                return authentication;
            }
        }

        public bool UserIsAuthenticated 
        {
            get
            {
                return userIsAuthenticated;
            }
        }

        public DataCenterService DataCenters
        {
            get
            {
                return dataCenters;
            }
        }

        public GroupService Groups
        {
            get
            {
                return groups;
            }
        }

        public QueueService Queues
        {
            get
            {
                return queues;
            }
        }

        public ServerService Servers
        {
            get
            {
                return servers;
            }
        }

        private async Task<UserInfo> AuthenticateUser(string userName, string password)
        {
            var authentication = new AuthenticationService();
            var result = await authentication.Login(userName, password).ConfigureAwait(false);
            return result;
        }

        private void InitializeServices()
        {
            authentication = new AuthenticationService();
            dataCenters = new DataCenterService(authenticationInfo);
            groups = new GroupService(authenticationInfo);
            queues = new QueueService(authenticationInfo);
            servers = new ServerService(authenticationInfo);
        }
    }
}
