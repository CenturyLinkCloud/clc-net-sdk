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
        public async Task<AccountBillingDetail> GetBillingDetails(CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Billing.GetAccountBillingDetails, Configuration.BaseUri, authentication.AccountAlias);
            return await GetBillingDetailsByLink(uri, cancellationToken).ConfigureAwait(false);
        }


        internal async Task<AccountBillingDetail> GetBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return await serviceInvoker.Invoke<AccountBillingDetail>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
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

        internal async Task<GroupBillingDetail> GetGroupBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return await serviceInvoker.Invoke<GroupBillingDetail>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }
        /*
        

        /// <summary>
        /// Gets unit pricing info for server resources.
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public async Task<ServerPricing> GetServerResourceUnitPricing(string serverId)
        {
            return await GetServerResourceUnitPricing(serverId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets unit pricing info for server resources.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ServerPricing> GetServerResourceUnitPricing(string serverId, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Billing.GetServerResourceUnitPricing, Configuration.BaseUri, authentication.AccountAlias, serverId));
            var result = await serviceInvoker.Invoke<ServerPricing>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Gets the total billing details for the account.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<AccountBillingDetail> GetAccountBillingDetailsByLink(string uri)
        {
            return await GetAccountBillingDetailsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        

        

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<BillingDetail> GetGroupBillingTotalsByLink(string uri)
        {
            return await GetGroupBillingTotalsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<BillingDetail> GetGroupBillingTotalsByLink(string uri, CancellationToken cancellationToken)
        {
            var billingDetail = new BillingDetail();

            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<GroupBillingDetail>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            foreach (var group in result.Groups)
            {
                foreach (var server in group.Value.Servers)
                {
                    billingDetail.MonthlyEstimate += server.Value.MonthlyEstimate;
                    billingDetail.CurrentHour += server.Value.CurrentHour;
                    billingDetail.MonthToDate += server.Value.MonthToDate;
                }
            }

            return billingDetail;
        }*/
    }
}
