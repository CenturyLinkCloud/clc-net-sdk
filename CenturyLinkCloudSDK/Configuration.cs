using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
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
            get { return baseUri; }
            set { baseUri = value; }
        }

        public static readonly IServiceInvoker DefaultServiceInvoker = new DefaultServiceInvoker();
        private static IServiceInvoker serviceInvoker = DefaultServiceInvoker;
        public static IServiceInvoker ServiceInvoker
        {
            get { return serviceInvoker; }
            set { serviceInvoker = value; }
        }

        private static IServiceResolver serviceResolver = new DefaultServiceResolver();
        internal static IServiceResolver ServiceResolver { get { return serviceResolver; } }
    }
}
