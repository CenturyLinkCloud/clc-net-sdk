using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses
{
    internal class LoginResponse: IServiceResponse
    {
        private UserInfo response = new UserInfo();

        public object Response 
        {           
            get
            {
                return response;
            }
            
            set
            {
                response = value as UserInfo;
            }
        }

    }
}
