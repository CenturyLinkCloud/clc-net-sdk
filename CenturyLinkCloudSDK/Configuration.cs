using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        private static IHttpMessageFormatter httpMessageFormatter = new DefaultHttpMessageFormatter();
        internal static IHttpMessageFormatter HttpMessageFormatter { get { return httpMessageFormatter; } }

        private static int maxConcurrentBulkHttpRequests = 2;
        /// <summary>
        /// Controls the maximum number of simultaneous requests used when performing bulk fetching operations.
        /// Currently GetServers and GetAlerts utilize this feature.  The default is 2.
        /// </summary>
        public static int MaxConcurrentBulkHttpRequests
        {
            get { return maxConcurrentBulkHttpRequests; }
            set { maxConcurrentBulkHttpRequests = value; }
        }

        private static List<ProductInfoHeaderValue> _additionalUserAgentValues = new List<ProductInfoHeaderValue>();
        public static List<ProductInfoHeaderValue> AdditionalUserAgentValues
        {
            get { return _additionalUserAgentValues; }
        }
    }
}
