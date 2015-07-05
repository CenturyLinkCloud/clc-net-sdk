using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BillingService : ServiceBase
    {        
        internal BillingService(Authentication authentication, IServiceInvoker serviceInvoker)
            : base(authentication, serviceInvoker)
        {            
        }

        /// <summary>
        /// Gets the total billing details for the account.
        /// </summary>
        /// <returns>The billing details for the account</returns>
        public Task<AccountBillingDetail> GetBillingDetails()
        {
            return GetBillingDetails(CancellationToken.None);
        }

        /// <summary>
        /// Gets the total billing details for the account.
        /// </summary>
        /// <returns>The billing details for the account</returns>
        public Task<AccountBillingDetail> GetBillingDetails(CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Billing.GetAccountBillingDetails, Configuration.BaseUri, authentication.AccountAlias);
            return GetBillingDetailsByLink(uri, cancellationToken);
        }


        internal Task<AccountBillingDetail> GetBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<AccountBillingDetail>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Gets the billing details of a data center.
        /// </summary>
        /// <param name="dataCenter">The data center</param>
        /// <returns>The billing details</returns>
        public Task<BillingDetail> GetBillingDetailsFor(DataCenter dataCenter)
        {
            return GetBillingDetailsFor(dataCenter, CancellationToken.None);
        }

        /// <summary>
        /// Gets the billing details of a data center.
        /// </summary>
        /// <param name="dataCenter">The data center</param>        
        /// <returns>The billing details</returns>
        public async Task<BillingDetail> GetBillingDetailsFor(DataCenter dataCenter, CancellationToken cancellationToken)
        {
            //TODO: validate this against the portal
            var rootGroup = await dataCenter.GetRootGroup(cancellationToken).ConfigureAwait(false);
            return await GetBillingDetailsFor(rootGroup, cancellationToken).ConfigureAwait(false);            
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="group">The group</param>
        /// <returns>The billing details</returns>
        public Task<BillingDetail> GetBillingDetailsFor(Group group)
        {
            return GetBillingDetailsFor(group, CancellationToken.None);
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="group">The group</param>        
        /// <returns>The billing details</returns>
        public async Task<BillingDetail> GetBillingDetailsFor(Group group, CancellationToken cancellationToken)
        {
            var uri = string.Format("{0}{1}", Configuration.BaseUri, group.BillingDetailsLink);
            var groupDetails = await GetGroupBillingDetailsByLink(uri, cancellationToken).ConfigureAwait(false);
                    
            return groupDetails.GetTotals();
        }

        /// <summary>
        /// Gets the billing details for a server.
        /// </summary>
        /// <param name="server">The server</param>
        /// <returns>The billing details</returns>
        public Task<BillingDetail> GetBillingDetailsFor(Server server)
        {
            return GetBillingDetailsFor(server, CancellationToken.None);
        }

        /// <summary>
        /// Gets the billing details for a server.
        /// </summary>
        /// <param name="server">The server</param>        
        /// <returns>The billing details</returns>
        public async Task<BillingDetail> GetBillingDetailsFor(Server server, CancellationToken cancellationToken)
        {          
            BillingDetail billingDetails = null;
            var uri = string.Format(Constants.ServiceUris.Billing.GetGroupBillingDetails, Configuration.BaseUri, authentication.AccountAlias, server.GroupId);
            var groupDetails = await GetGroupBillingDetailsByLink(uri, cancellationToken).ConfigureAwait(false);
            if (groupDetails.Groups.ContainsKey(server.GroupId))
            {
                var parentGroupDetails = groupDetails.Groups[server.GroupId];
                if(parentGroupDetails.Servers.ContainsKey(server.Id))
                {
                    billingDetails = parentGroupDetails.Servers[server.Id];
                }
            }
            
            return billingDetails;
        }

        internal Task<GroupBillingDetail> GetGroupBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<GroupBillingDetail>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Gets unit pricing info for server resources.
        /// </summary>
        /// <param name="serverId">The server id</param>
        /// <returns>The pricing for the server</returns>
        public Task<ServerPricing> GetServerResourceUnitPricing(string serverId)
        {
            return GetServerResourceUnitPricing(serverId, CancellationToken.None);
        }

        /// <summary>
        /// Gets unit pricing info for server resources.
        /// </summary>
        /// <param name="serverId">The server id</param>        
        /// <returns>The pricing for the server</returns>
        public Task<ServerPricing> GetServerResourceUnitPricing(string serverId, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Billing.GetServerResourceUnitPricing, Configuration.BaseUri, authentication.AccountAlias, serverId));
            return serviceInvoker.Invoke<ServerPricing>(httpRequestMessage, cancellationToken);
        }
    }
}
