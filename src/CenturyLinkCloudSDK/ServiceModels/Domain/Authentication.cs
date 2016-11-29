using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Authentication
    {
        public string BearerToken { get; set; }

        public string AccountAlias { get; set; }

        public string LocationAlias { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
