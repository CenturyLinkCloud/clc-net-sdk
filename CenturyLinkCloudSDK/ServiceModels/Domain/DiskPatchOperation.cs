using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class DiskPatchOperation
    {
        public string Op { get; set; }

        public string Member { get; set; }

        public IEnumerable<DiskPatch> Value { get; set; }      
    }
}
