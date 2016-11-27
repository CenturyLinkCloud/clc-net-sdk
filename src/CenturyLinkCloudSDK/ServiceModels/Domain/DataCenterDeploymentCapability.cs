using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class DataCenterDeploymentCapability
    {
        public bool DataCenterEnabled { get; set; }

        public bool ImportVMEnabled { get; set; }

        public bool SupportsPremiumStorage { get; set; }

        public bool SupportsSharedLoadBalancer { get; set; }

        public IEnumerable<DeployableNetwork> DeployableNetworks { get; set; }

        public IEnumerable<Template> Templates { get; set; }

        public IEnumerable<ImportableOsType> ImportableOsTypes { get; set; }
    }
}
