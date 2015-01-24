using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses
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
