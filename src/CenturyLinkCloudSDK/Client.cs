using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.Runtime;

namespace CenturyLinkCloudSDK
{
    /// <summary>
    /// This is the facade that provides access to all API services.
    /// </summary>
    public class Client
    {                
        /// <summary>
        /// Constructor called when the user needs to be authenticated. It sets the userInfo and authenticationInfo fields
        /// that can then be accessed by the corresponding readonly properties. The UserInfo for User Info display and the AuthenticationInfo
        /// for subsequent calls to the API after the authentication has succeeded.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public Client(string userName, string password)
        {
            UserIsAuthenticated = false;

            var userInfo = AuthenticateUser(userName, password).Result;

            if (userInfo != null)
            {
                UserIsAuthenticated = true;
                Authentication = new Authentication() 
                { 
                    AccountAlias = userInfo.AccountAlias, 
                    BearerToken = userInfo.BearerToken, 
                    LocationAlias = userInfo.LocationAlias,                    
                    Roles = userInfo.Roles
                };

                InitializeServices();
            }
            else
            {
                UserIsAuthenticated = false;
            }
        }

        /// <summary>
        /// Constructor called after an authentication request has succeeded. The AuthenticationInfo object
        /// contains properties for Account Alias and BearerToken.
        /// </summary>
        /// <param name="authenticationInfo"></param>
        public Client(Authentication authenticationInfo)
        {
            if (authenticationInfo != null)
            {
                UserIsAuthenticated = true;
                Authentication = authenticationInfo;
                InitializeServices();
            }
            else
            {
                UserIsAuthenticated = false;
            }
        }
        
        public Authentication Authentication { get; private set; }
        public bool UserIsAuthenticated { get; private set; }

        public DataCenterService DataCenters { get; private set; }        
        public GroupService Groups { get; private set; }
        public QueueService Queues { get; private set; }
        public ServerService Servers { get; private set; }        
        public AlertService Alerts { get; private set; }
        public BillingService Billing { get; private set; }
        public AccountService Account { get; private set; }

        private async Task<UserInfo> AuthenticateUser(string userName, string password)
        {
            var authenticationService = Configuration.ServiceResolver.Resolve<AuthenticationService>(null);
            var result = await authenticationService.Login(userName, password).ConfigureAwait(false);
            return result;
        }

        private void InitializeServices()
        {
            var invoker = Configuration.ServiceInvoker;
            var resolver = Configuration.ServiceResolver;            
            DataCenters = resolver.Resolve<DataCenterService>(Authentication, invoker);
            Groups = resolver.Resolve<GroupService>(Authentication, invoker);
            Queues = resolver.Resolve<QueueService>(Authentication, invoker);
            Servers = resolver.Resolve<ServerService>(Authentication, invoker);
            Alerts = resolver.Resolve<AlertService>(Authentication, invoker);
            Billing = resolver.Resolve<BillingService>(Authentication, invoker);
            Account = resolver.Resolve<AccountService>(Authentication, invoker);
        }
    }
}
