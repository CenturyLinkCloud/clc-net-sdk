using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class PublicIpAddress
    {
        public string InternalIPAddress { get; set; }

        public IEnumerable<PortDetail> Ports { get; set; }

        public IEnumerable<SourceRestriction> SourceRestrictions { get; set; }
    }
}
