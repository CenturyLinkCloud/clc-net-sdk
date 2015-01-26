using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using System;

namespace CenturyLinkCloudSDK.ServiceAPI.Runtime
{
    /// <summary>
    /// This class is used as a static repository of authentication information that needs to persist accross requests.
    /// The UserInfo property contains the BearerToken needed to be passed to all API calls.
    /// </summary>
    public static class Authentication
    {
        private static Lazy<bool> isUserAuthenticated = new Lazy<bool>(() => false);
        private static Lazy<UserInfo> userInfo = null;

        public static UserInfo UserInfo 
        {
            get { return userInfo.Value; }

            set 
            {
                userInfo = new Lazy<UserInfo>(() => value);

                if (value != null)
                {
                    isUserAuthenticated = new Lazy<bool>(() => true);
                }
                else
                {
                    isUserAuthenticated = new Lazy<bool>(() => false);                    
                }
            }
        }

        public static bool IsUserAuthenticated
        {
            get { return isUserAuthenticated.Value; }
        }
    }
}
