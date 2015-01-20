using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    public class ServerOperation
    {
        public string Server { get; set; }

        public bool IsQueued { get; set; }

        public List<Link> Links { get; set; }

        public string ErrorMessage { get; set; }
    }
}
