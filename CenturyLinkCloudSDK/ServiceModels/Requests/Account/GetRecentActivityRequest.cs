using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.Requests.Account
{
    internal class GetRecentActivityRequest
    {
        public IEnumerable<string> Accounts { get; set; }

        public int Limit { get; set; }
    }
}
