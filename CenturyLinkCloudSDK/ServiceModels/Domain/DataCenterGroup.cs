using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class DataCenterGroup
    {
        private string rootHardwareGroupLink;

        internal Authentication Authentication { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyAttribute]
        private IReadOnlyList<Link> Links { get; set; }

        /// <summary>
        /// Determines if this data center group has a root hardware group by examining the Links collection.
        /// </summary>
        public bool HasRootHardwareGroup
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

        /// <summary>
        /// Gets the root hardware group.
        /// </summary>
        /// <returns>Asyncronous Task of Group</returns>
        public async Task<Group> GetRootHardwareGroup()
        {
            return await GetRootHardwareGroup(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the root hardware group.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Asyncronous Task of Group</returns>
        public async Task<Group> GetRootHardwareGroup(CancellationToken cancellationToken)
        {
            if (!HasRootHardwareGroup)
            {
                throw new InvalidOperationException(string.Format(Constants.ExceptionMessages.DataCenterGroupDoesNotHaveRootHardwareGroup, Name));
            }

            var groupService = new GroupService(Authentication);
            var rootGroup = await groupService.GetGroupByLink(Configuration.BaseUri + rootHardwareGroupLink, cancellationToken);
            return rootGroup;
        }
    }
}
