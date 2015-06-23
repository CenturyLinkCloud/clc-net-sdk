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
        DataCenterService dataCenterService;
        internal BillingService(Authentication authentication, IServiceInvoker serviceInvoker, DataCenterService dataCenterService)
            : base(authentication, serviceInvoker)
        {
            this.dataCenterService = dataCenterService;
        }

        /// <summary>
        /// Gets the total billing details for the account.
        /// </summary>
        /// <returns></returns>
        public async Task<AccountBillingDetail> GetAccountBillingDetails()
        {
            return await GetAccountBillingDetails(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// ets the total billing details for the account.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AccountBillingDetail> GetAccountBillingDetails(CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Billing.GetAccountBillingDetails, Configuration.BaseUri, authentication.AccountAlias);
            return await GetAccountBillingDetailsByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details of a data center.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetDataCenterBillingDetails(string dataCenterId)
        {
            return await GetDataCenterBillingDetails(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details of a data center.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetDataCenterBillingDetails(string dataCenterId, CancellationToken cancellationToken)
        {
            var billingDetail = new BillingDetail();            

            var dataCenterGroup = await dataCenterService.GetDataCenterGroup(dataCenterId).ConfigureAwait(false);
            var rootGroup = await dataCenterGroup.GetRootHardwareGroup(cancellationToken).ConfigureAwait(false);
            var groupBillingDetail = await GetGroupBillingDetails(rootGroup.Id).ConfigureAwait(false);

            foreach (var group in groupBillingDetail.Groups)
            {
                foreach (var server in group.Value.Servers)
                {
                    billingDetail.MonthlyEstimate += server.Value.MonthlyEstimate;
                    billingDetail.CurrentHour += server.Value.CurrentHour;
                    billingDetail.MonthToDate += server.Value.MonthToDate;
                }
            }

            return billingDetail;
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<GroupBillingDetail> GetGroupBillingDetails(string groupId)
        {
            return await GetGroupBillingDetails(groupId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GroupBillingDetail> GetGroupBillingDetails(string groupId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Billing.GetGroupBillingDetails, Configuration.BaseUri, authentication.AccountAlias, groupId);
            return await GetGroupBillingDetailsByLink(uri, cancellationToken).ConfigureAwait(false);
        }

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
        /// Gets the total billing details for the account.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<AccountBillingDetail> GetAccountBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<AccountBillingDetail>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<GroupBillingDetail> GetGroupBillingDetailsByLink(string uri)
        {
            return await GetGroupBillingDetailsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details for a group.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<GroupBillingDetail> GetGroupBillingDetailsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<GroupBillingDetail>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
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
        }
    }
}
