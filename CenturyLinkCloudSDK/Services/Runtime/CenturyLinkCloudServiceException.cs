using CenturyLinkCloudSDK.ServiceModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services.Runtime
{
    public class CenturyLinkCloudServiceException : Exception
    {
        public CenturyLinkCloudServiceException()
        {
        }

        public CenturyLinkCloudServiceException(string message) : base(message)
        {
        }

        public CenturyLinkCloudServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        public string ServiceUri { get; set; }

        public string HttpMethod { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public string ApiMessage { get; set; }

    }
}
