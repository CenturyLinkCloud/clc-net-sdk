using CenturyLinkCloudSDK.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK
{
    public static class Configuration
    {
        private static string baseUri = Constants.ServiceUris.ApiBaseAddress;

        public static string BaseUri 
        {
            get
            {
                return baseUri;
            }

            set
            {
                baseUri = value;
            }
        }
    }
}
