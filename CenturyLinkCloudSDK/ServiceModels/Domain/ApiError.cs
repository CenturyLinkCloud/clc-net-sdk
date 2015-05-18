using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class ApiError
    {
        public string Message { get; set; }

        public IDictionary<string, List<string>> ModelState { get; set; }
    }
}
