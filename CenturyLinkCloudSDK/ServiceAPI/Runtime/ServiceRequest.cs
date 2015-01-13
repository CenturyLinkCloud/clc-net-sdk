using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Net.Http;

namespace CenturyLinkCloudSDK.ServiceAPI.Runtime
{
    public class ServiceRequest
    {
        public string BaseAddress { get; set; }

        public string  ServiceUri { get; set; }

        public string MediaType { get; set; }

        public IServiceRequestModel RequestModel { get; set; }

        public HttpMethod HttpMethod { get; set; }
    }
}
