﻿using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.Extensions;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with data centers.
    /// </summary>
    public class DataCenterService : ServiceBase
    {
        GroupService groupService;
        AccountService accountService;
        BillingService billingService;
        internal DataCenterService(Authentication authentication, IServiceInvoker serviceInvoker, GroupService groupService, AccountService accountService, BillingService billingService)
            : base(authentication, serviceInvoker)
        {
            this.groupService = groupService;
            this.accountService = accountService;
            this.billingService = billingService;
        }

        void SetInternalDataCenterProperties(DataCenter dc, bool includeTotalAssets)
        {
            dc.HasTotalAssets = includeTotalAssets;
            dc.GroupService = groupService;
            dc.DataCenterService = this;
        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// </summary>
        /// <param name="includeTotalAssets">Whether or not to include total assets.  Including
        /// total assets is a more time consuming operation</param>
        /// <returns>The data centers for the account</returns>
        public Task<IEnumerable<DataCenter>> GetDataCenters(bool includeTotalAssets)
        {
            return GetDataCenters(includeTotalAssets, CancellationToken.None);
        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// </summary>        
        /// <param name="includeTotalAssets">Whether or not to include total assets.  Including
        /// total assets is a more time consuming operation</param>
        /// <returns>The data centers for the account</returns>
        public async Task<IEnumerable<DataCenter>> GetDataCenters(bool includeTotalAssets, CancellationToken cancellationToken)
        {
            var httpRequestMessage = 
                CreateAuthorizedHttpRequestMessage(
                    HttpMethod.Get, 
                    string.Format(
                        Constants.ServiceUris.DataCenter.GetDataCenters, 
                        Configuration.BaseUri, 
                        authentication.AccountAlias, 
                        includeTotalAssets ? 
                            Constants.ServiceUris.Querystring.IncludeGroupLinksAndTotalAssets :
                            Constants.ServiceUris.Querystring.IncludeGroupLinks));
            var result = await serviceInvoker.Invoke<IEnumerable<DataCenter>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            foreach (var dc in result) SetInternalDataCenterProperties(dc, includeTotalAssets);

            return result;
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="dataCenterId">The id of the data center</param>
        /// <param name="includeTotalAssets">Whether or not to include total assets.  Including
        /// total assets is a more time consuming operation</param>
        /// <returns>The data center</returns>
        public Task<DataCenter> GetDataCenter(string dataCenterId, bool includeTotalAssets)
        {
            return GetDataCenter(dataCenterId, includeTotalAssets, CancellationToken.None);
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="dataCenterId">The id of the data center</param>
        /// <param name="includeTotalAssets">Whether or not to include total assets.  Including
        /// total assets is a more time consuming operation</param>
        /// <returns>The data center</returns>
        public Task<DataCenter> GetDataCenter(string dataCenterId, bool includeTotalAssets, CancellationToken cancellationToken)
        {
            var uri =
                string.Format(
                    Constants.ServiceUris.DataCenter.GetDataCenter,
                    Configuration.BaseUri,
                    authentication.AccountAlias,
                    dataCenterId,
                    includeTotalAssets ?
                        Constants.ServiceUris.Querystring.IncludeGroupLinksAndTotalAssets :
                        Constants.ServiceUris.Querystring.IncludeGroupLinks);
            return GetDataCenterByLink(uri, includeTotalAssets, cancellationToken);            
        }

        internal async Task<DataCenter> GetDataCenterByLink(string uri, bool includeTotalAssets, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<DataCenter>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            SetInternalDataCenterProperties(result, includeTotalAssets);

            return result;
        }

        /// <summary>
        /// Gets the data center overview, which contains composite information from several different areas such as billing, compute limits recent activities etc.
        /// </summary>
        /// <param name="dataCenterId">The id of the data center</param>
        /// <returns>A detailed overview of the data center</returns>
        public Task<DataCenterOverview> GetDataCenterOverview(string dataCenterId)
        {
            return GetDataCenterOverview(dataCenterId, CancellationToken.None);
        }

        /// <summary>
        /// Gets the data center overview, which contains composite information from several different areas such as billing, compute limits recent activities etc.
        /// </summary>
        /// <param name="dataCenterId">The id of the data center</param>        
        /// <returns>A detailed overview of the data center</returns>
        public async Task<DataCenterOverview> GetDataCenterOverview(string dataCenterId, CancellationToken cancellationToken)
        {
            var dataCenter = await GetDataCenter(dataCenterId, true, cancellationToken).ConfigureAwait(false);

            if (dataCenter != null)
            {
                var tasks = new List<Task>();

                BillingDetail billingTotals = null;
                ComputeLimits computeLimits = null;                
                DefaultSettings defaultSettings = null;
                NetworkLimits networkLimits = null;
                IEnumerable<Activity> recentActivity = null;

                tasks.Add(Task.Run(async () => billingTotals = await billingService.GetBillingDetailsFor(dataCenter).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => computeLimits = await dataCenter.GetComputeLimits(cancellationToken).ConfigureAwait(false)));                
                tasks.Add(Task.Run(async () => defaultSettings = await dataCenter.GetDefaultSettings(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => networkLimits = await dataCenter.GetNetworkLimits(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => recentActivity = await accountService.GetRecentActivityFor(dataCenter, cancellationToken).ConfigureAwait(false)));

                await Task.WhenAll(tasks).ConfigureAwait(false);


                var dataCenterOverview = new DataCenterOverview()
                {
                    DataCenter = dataCenter,
                    BillingTotals = billingTotals,
                    ComputeLimits = computeLimits,                    
                    DefaultSettings = defaultSettings,
                    NetworkLimits = networkLimits,
                    RecentActivity = recentActivity
                };

                return dataCenterOverview;
            }

            return null;
        }
        
        internal async Task<ComputeLimits> GetComputeLimitsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<ComputeLimits>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                result.MemoryGBFormatted = result.MemoryGB.Value.RoundNumberToNearestUpperLimit();
                result.StorageGBFormatted = result.StorageGB.Value.RoundNumberToNearestUpperLimit();
            }

            return result;
        }
      
        internal Task<NetworkLimits> GetNetworkLimitsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<NetworkLimits>(httpRequestMessage, cancellationToken);
        } 

        internal Task<DataCenterDeploymentCapability> GetDeploymentCapabilitiesByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<DataCenterDeploymentCapability>(httpRequestMessage, cancellationToken);
        }        
    }
}
