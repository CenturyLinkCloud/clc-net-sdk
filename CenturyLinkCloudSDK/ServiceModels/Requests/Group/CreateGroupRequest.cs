using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.Requests.Group
{
    public class CreateGroupRequest
    {
        public CreateGroupRequest(string name, string parentGroupId)
        {
            Name = name;
            ParentGroupId = parentGroupId;
        }

        public string Name { get; private set; }
        public string ParentGroupId { get; private set; }
        public string Description { get; set; }
        public IEnumerable<CustomField> CustomFields { get; set; }
    }
}
