﻿using System;
using System.Net.Http;

namespace CenturyLinkCloudSDK.Runtime
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