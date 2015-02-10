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
        private static string baseUri = null;

        public static string BaseUri 
        {
            get 
            {
                if (string.IsNullOrEmpty(baseUri))
                {
                    return Constants.ServiceUris.ApiBaseAddress;
                }
                else
                {
                    return baseUri;
                }
            }

            set 
            {
                baseUri = value;
            }
        }
    }
}
