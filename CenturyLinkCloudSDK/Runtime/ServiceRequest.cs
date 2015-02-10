using System.Net.Http;

namespace CenturyLinkCloudSDK.Runtime
{
    /// <summary>
    /// This is the base class all service requests must inherit from.
    /// </summary>
    internal class ServiceRequest
    {
        public string  ServiceUri { get; set; }

        public string BearerToken { get; set; }

        public ServiceRequestModel RequestModel { get; set; }

        public HttpMethod HttpMethod { get; set; }
    }
}
