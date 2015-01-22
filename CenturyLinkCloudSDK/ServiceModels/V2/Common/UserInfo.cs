using System;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class UserInfo
    {
        public string UserName { get; set; }

        public string AccountAlias { get; set; }

        public string LocationAlias { get; set; }

        public string[] Roles { get; set; }

        public string BearerToken { get; set; }
    }
}
