using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class passed in with the Client that contains authentication information.
    /// </summary>
    public class AuthenticationInfo
    {
        public string BearerToken { get; set; }

        public string AccountAlias { get; set; }
    }
}
