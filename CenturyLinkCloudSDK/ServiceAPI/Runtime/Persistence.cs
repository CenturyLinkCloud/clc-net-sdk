using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.Runtime
{
    public static class Persistence
    {
        private static Lazy<bool> isUserAuthenticated = new Lazy<bool>(() => false);
        private static Lazy<LoginResponse> userInfo = null;

        public static LoginResponse UserInfo 
        {
            get { return userInfo.Value; }

            set 
            {
                userInfo = new Lazy<LoginResponse>(() => value);

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
