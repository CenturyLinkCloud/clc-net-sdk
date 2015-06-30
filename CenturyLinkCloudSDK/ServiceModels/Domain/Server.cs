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
    public class Server
    {
        Lazy<Link> statisticsLink;
        Lazy<Link> credentialsLink;

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Server()
        {
            statisticsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "statistics", StringComparison.CurrentCultureIgnoreCase));
            });

            credentialsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "credentials", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public bool IsTemplate { get; set; }
        public string LocationId { get; set; }
        public string OsType { get; set; }
        public string Status { get; set; }
        public ServerDetail Details { get; set; }
        public string Type { get; set; }
        public string StorageType { get; set; }
        public ChangeInfo ChangeInfo { get; set; }

        internal ServerService ServerService { get; set; }

        /// <summary>
        /// Determines if this server has statistics
        /// </summary>
        public bool HasStatistics
        {
            get { return statisticsLink.Value != null; }
        }

        /// <summary>
        /// Determines if the credentials link is available.
        /// </summary>
        /// <returns></returns>
        public bool HasCredentials
        {
            get { return credentialsLink.Value != null; }
        }

        /// <summary>
        /// Gets the server credentials.
        /// </summary>
        /// <returns>The credentials</returns>
        public Task<ServerCredential> GetCredentials()
        {
            return GetCredentials(CancellationToken.None);
        }

        /// <summary>
        /// Gets the server credentials.
        /// </summary>        
        /// <returns>The credentials</returns>
        public async Task<ServerCredential> GetCredentials(CancellationToken cancellationToken)
        {
            if (!HasCredentials)
            {
                return null;
            }

            return await ServerService.GetServerCredentialsByLink(string.Format("{0}{1}", Configuration.BaseUri, credentialsLink.Value.Href), cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the statistics for this server
        /// </summary>
        /// <returns>The statistics</returns>
        public Task<Statistics> GetStatistics()
        {
            return GetStatistics(CancellationToken.None);
        }

        /// <summary>
        /// Gets the statistics for this server
        /// </summary>
        /// <returns>The statistics</returns>
        public async Task<Statistics> GetStatistics(CancellationToken cancellationToken)
        {
            if(!HasStatistics)
            {
                return null;
            }

            return await ServerService.GetServerStatisticsByLink(string.Format("{0}{1}{2}", Configuration.BaseUri, statisticsLink.Value.Href, Constants.ServiceUris.Querystring.GetLatestStatistics), cancellationToken).ConfigureAwait(false);
        }        
    }
}
