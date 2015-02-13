using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class DataCenterGroup
    {
        private string rootHardwareGroupLink;

        public AuthenticationInfo UserAuthentication { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyAttribute]
        private IReadOnlyList<Link> Links { get; set; }

        public bool HasRootGroup
        {
            get
            {
                if (Links != null)
                {
                    var rootHardwareGroupLink = Links.Where(l => l.Rel.ToUpper() == "GROUP").FirstOrDefault();

                    if (rootHardwareGroupLink != null)
                    {
                        this.rootHardwareGroupLink = rootHardwareGroupLink.Href;
                        return true;
                    }
                }

                return false;
            }
        }

        public async Task<Group> GetRootHardwareGroup()
        {
            if (!HasRootGroup)
            {
                throw new InvalidOperationException(string.Format(Constants.ExceptionMessages.DataCenterGroupDoesNotHaveRootHardwareGroup, Name));
            }

            var groupService = new GroupService(UserAuthentication);
            var rootGroup = await groupService.GetGroupByHyperLink(rootHardwareGroupLink);
            return rootGroup;
        }
    }
}
