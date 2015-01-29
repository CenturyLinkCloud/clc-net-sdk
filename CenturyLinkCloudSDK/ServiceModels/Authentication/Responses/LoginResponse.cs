using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.Authentication.Responses
{
    /// <summary>
    /// This class contains the response from the Login operation.
    /// </summary>
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
