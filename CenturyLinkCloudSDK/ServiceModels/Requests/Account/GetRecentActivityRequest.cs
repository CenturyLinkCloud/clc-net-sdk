using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.Requests.Account
{
    internal class GetRecentActivityRequest
    {
        public IEnumerable<string> Accounts { get; set; }

        public int Limit { get; set; }

        public IEnumerable<string> EntityTypes { get; set; }

        public IEnumerable<string> EntityIds { get; set; }

        public IEnumerable<string> ReferenceIds { get; set; }

    }
}
