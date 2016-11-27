using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.Account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains account-level operations.
    /// </summary>
    public class AccountService : ServiceBase
    {
        static readonly string[] ServerAndGroupEntityTypes = new[] { Constants.EntityTypes.Server, Constants.EntityTypes.Group };
        
        internal AccountService(Authentication authentication, IServiceInvoker serviceInvoker)
            : base(authentication, serviceInvoker)
        {

        }

        #region Get global recent activity
        /// <summary>
        /// Returns recent account activity.
        /// </summary>        
        /// <returns>The recent activity for the account</returns>
        public Task<IEnumerable<Activity>> GetRecentActivity()
        {
            return GetRecentActivity(CancellationToken.None);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>  
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The recent activity for the account</returns>
        public Task<IEnumerable<Activity>> GetRecentActivity(int recordLimit)
        {
            return GetRecentActivity(recordLimit, CancellationToken.None);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <returns>The recent activity for the account</returns>
        public Task<IEnumerable<Activity>> GetRecentActivity(CancellationToken cancellationToken)
        {
            return GetRecentActivity(10, cancellationToken);
        }
       
        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The recent activity for the account</returns>
        public Task<IEnumerable<Activity>> GetRecentActivity(int recordLimit, CancellationToken cancellationToken)
        {
            return GetRecentActivity(null, recordLimit, cancellationToken);
        }
        #endregion

        #region Get data center recent activity
        /// <summary>
        /// Returns the recent activity for the indicated data center
        /// </summary>
        /// <param name="dc">The data center</param>
        /// <returns>The data center recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(DataCenter dc)
        {
            return GetRecentActivityFor(dc, CancellationToken.None);
        }

        /// <summary>
        /// Returns the recent activity for the indicated data center
        /// </summary>
        /// <param name="dc">The data center</param>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The data center recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(DataCenter dc, int recordLimit)
        {
            return GetRecentActivityFor(dc, recordLimit, CancellationToken.None);
        }

        /// <summary>
        /// Returns the recent activity for the indicated data center
        /// </summary>
        /// <param name="dc">The data center</param>
        /// <returns>The data center recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(DataCenter dc, CancellationToken cancellationToken)
        {
            return GetRecentActivityFor(dc, 10, cancellationToken);
        }

        /// <summary>
        /// Returns the recent activity for the indicated data center
        /// </summary>
        /// <param name="dc">The data center</param>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The data center recent activity</returns>
        public async Task<IEnumerable<Activity>> GetRecentActivityFor(DataCenter dc, int recordLimit, CancellationToken cancellationToken)
        {
            var rootGroup = await dc.GetRootGroup(cancellationToken).ConfigureAwait(false);
            return await GetRecentActivityFor(rootGroup, recordLimit, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Get group recent activity
        /// <summary>
        /// Returns the recent activity for the indicated group
        /// </summary>
        /// <param name="g">The group</param>
        /// <returns>The group recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Group g)
        {
            return GetRecentActivityFor(g, CancellationToken.None);
        }

        /// <summary>
        /// Returns the recent activity for the indicated group
        /// </summary>
        /// <param name="g">The group</param>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The group recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Group g, int recordLimit)
        {
            return GetRecentActivityFor(g, recordLimit, CancellationToken.None);
        }

        /// <summary>
        /// Returns the recent activity for the indicated group
        /// </summary>
        /// <param name="g">The group</param>
        /// <returns>The group recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Group g, CancellationToken cancellationToken)
        {
            return GetRecentActivityFor(g, 10, cancellationToken);
        }

        /// <summary>
        /// Returns the recent activity for the indicated group
        /// </summary>
        /// <param name="g">The group</param>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The group recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Group g, int recordLimit, CancellationToken cancellationToken)
        {
            var groupAndServerIds = new List<string>();
            g.AppendContainedGroupAndServerIds(groupAndServerIds);
            return GetRecentActivity(groupAndServerIds, recordLimit, cancellationToken);
        }
        #endregion

        #region Get server recent activity
        /// <summary>
        /// Returns the recent activity for the indicated server
        /// </summary>
        /// <param name="s">The server</param>
        /// <returns>The server recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Server s)
        {
            return GetRecentActivityFor(s, CancellationToken.None);
        }

        /// <summary>
        /// Returns the recent activity for the indicated server
        /// </summary>
        /// <param name="s">The server</param>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The server recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Server s, int recordLimit)
        {
            return GetRecentActivityFor(s, recordLimit, CancellationToken.None);
        }

        /// <summary>
        /// Returns the recent activity for the indicated server
        /// </summary>
        /// <param name="s">The server</param>
        /// <returns>The server recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Server s, CancellationToken cancellationToken)
        {
            return GetRecentActivityFor(s, 10, cancellationToken);
        }

        /// <summary>
        /// Returns the recent activity for the indicated server
        /// </summary>
        /// <param name="s">The server</param>
        /// <param name="recordLimit">The maximum number of records to return</param>
        /// <returns>The server recent activity</returns>
        public Task<IEnumerable<Activity>> GetRecentActivityFor(Server s, int recordLimit, CancellationToken cancellationToken)
        {
            return GetRecentActivity(new[] { s.Id }, recordLimit, cancellationToken);
        }
        #endregion

        Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordLimit, CancellationToken cancellationToken)
        {
            var accounts = new List<String> { authentication.AccountAlias };
            var requestModel = 
                new GetRecentActivityRequest
                { 
                    Accounts = accounts, 
                    Limit = recordLimit,
                    ReferenceIds = referenceIds,
                    EntityTypes = (referenceIds == null) ? null : ServerAndGroupEntityTypes
                };

            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Account.GetRecentActivity, Configuration.BaseUri), requestModel);
            return serviceInvoker.Invoke<IEnumerable<Activity>>(httpRequestMessage, cancellationToken);
        }
    }
}
