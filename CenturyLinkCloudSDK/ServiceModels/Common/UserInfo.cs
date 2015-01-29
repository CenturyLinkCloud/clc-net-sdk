using System;

namespace CenturyLinkCloudSDK.ServiceModels.Common
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class UserInfo
    {
        public string UserName { get; set; }

        public string AccountAlias { get; set; }

        public string LocationAlias { get; set; }

        public string[] Roles { get; set; }

        public string BearerToken { get; set; }
    }
}
