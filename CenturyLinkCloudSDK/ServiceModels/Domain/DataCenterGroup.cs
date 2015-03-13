using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class DataCenterGroup
    {

        private Lazy<string> rootHardwareGroupLink;

        public Authentication Authentication { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataCenterGroup()
        {
            rootHardwareGroupLink = new Lazy<string>(() =>
            {
                var groupLink = Links.FirstOrDefault(l => String.Equals(l.Rel, "group", StringComparison.CurrentCultureIgnoreCase));
                return groupLink != null ? groupLink.Href : null;
            });
        }

        /// <summary>
        /// Determines if this data center group has a root hardware group by examining the Links collection.
        /// </summary>
        public bool HasRootHardwareGroup
        {
            get
            {
                return !string.IsNullOrEmpty(rootHardwareGroupLink.Value);
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
            var rootGroup = await groupService.GetGroupByLink(Configuration.BaseUri + rootHardwareGroupLink.Value, cancellationToken);
            return rootGroup;
        }

    }
}
