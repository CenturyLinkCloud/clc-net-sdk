using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.Responses.Servers
{
    public class CreateServerResponse
    {
        public string Server { get; set; }

        public bool IsQueued { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public string ErrorMessage { get; set; }
    }
}
