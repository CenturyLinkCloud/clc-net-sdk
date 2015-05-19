using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.DataCenter;
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
        internal DataCenterService(Authentication authentication)
            : base(authentication)
        {

        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// Use this operation when you need the list of data center names and codes that you have access to. 
        /// Using that list of data centers, you can then query for the root group, and all the child groups in an entire data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataCenter>> GetDataCenters()
        {
            return await GetDataCenters(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// Use this operation when you need the list of data center names and codes that you have access to. 
        /// Using that list of data centers, you can then query for the root group, and all the child groups in an entire data center.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataCenter>> GetDataCenters(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenters, Configuration.BaseUri, authentication.AccountAlias, string.Empty));
            var result = await ServiceInvoker.Invoke<IEnumerable<DataCenter>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenter(string dataCenterId)
        {
            return await GetDataCenter(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenter(string dataCenterId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, string.Empty);
            return await GetDataCenterByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenterId)
        {
            return await GetDataCenterGroup(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenterId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, Constants.ServiceUris.Querystring.IncludeGroupLinks);
            return await GetDataCenterGroupByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the data center overview, which contains composite information from several different areas such as billing, compute limits recent activities etc.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenterOverview> GetDataCenterOverview(string dataCenterId)
        {
            return await GetDataCenterOverview(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the data center overview, which contains composite information from several different areas such as billing, compute limits recent activities etc.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenterOverview> GetDataCenterOverview(string dataCenterId, CancellationToken cancellationToken)
        {
            var dataCenter = await GetDataCenterWithGroupsAndTotalAssets(dataCenterId, cancellationToken).ConfigureAwait(false);

            if (dataCenter != null)
            {
                var tasks = new List<Task>();

                BillingDetail billingTotals = null;
                ComputeLimits computeLimits = null;
                Group rootGroup = null;
                DefaultSettings defaultSettings = null;
                NetworkLimits networkLimits = null;
                List<string> serverIds = null;
                IEnumerable<Activity> recentActivity = null;

                tasks.Add(Task.Run(async () => billingTotals = await dataCenter.GetBillingTotals(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => computeLimits = await dataCenter.GetComputeLimits(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => rootGroup = await dataCenter.GetRootGroup(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => defaultSettings = await dataCenter.GetDefaultSettings(cancellationToken).ConfigureAwait(false)));
                tasks.Add(Task.Run(async () => networkLimits = await dataCenter.GetNetworkLimits(cancellationToken).ConfigureAwait(false)));

                await Task.WhenAll(tasks);

                if (rootGroup != null)
                {
                    var groupService = new GroupService(authentication);

                    serverIds = groupService.GetServerIds(rootGroup, new List<string>());
                    recentActivity = await GetRecentActivity(serverIds, cancellationToken).ConfigureAwait(false);
                }

                var dataCenterOverview = new DataCenterOverview()
                {
                    DataCenter = dataCenter,
                    BillingTotals = billingTotals,
                    ComputeLimits = computeLimits,
                    RootGroup = rootGroup,
                    DefaultSettings = defaultSettings,
                    NetworkLimits = networkLimits,
                    RecentActivity = recentActivity
                };

                return dataCenterOverview;
            }

            return null;
        }     

        /// <summary>
        ///  Gets the total assets for all data centers.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataCenter>> GetAllDataCentersWithTotalAssets()
        {
            return await GetAllDataCentersWithTotalAssets(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the total assets for all data centers.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<DataCenter>> GetAllDataCentersWithTotalAssets(CancellationToken cancellationToken)
        //{
        //    var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenters, Configuration.BaseUri, authentication.AccountAlias, Constants.ServiceUris.Querystring.IncludeGroupLinksAndTotalAssets));
        //    var result = await ServiceInvoker.Invoke<IEnumerable<DataCenter>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

        //    return result;
        //}

        /// <summary>
        /// Gets the total assets for all data centers.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataCenter>> GetAllDataCentersWithTotalAssets(CancellationToken cancellationToken)
        {
            var dataCentersWithTotals = new List<DataCenter>();
            var tasks = new List<Task<DataCenter>>();

            var dataCenterService = new DataCenterService(authentication);
            var dataCenters = await dataCenterService.GetDataCenters().ConfigureAwait(false);

            foreach (var dataCenter in dataCenters)
            {
                tasks.Add(Task.Run(async () => await dataCenterService.GetDataCenterWithTotalAssets(dataCenter.Id, cancellationToken).ConfigureAwait(false)));
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                dataCentersWithTotals.Add(task.Result);
            }

            return dataCentersWithTotals;
        }

        /// <summary>
        /// Gets a data center with total asset information.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenterWithTotalAssets(string dataCenterId)
        {
            return await GetDataCenterWithTotalAssets(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a data center with total asset information.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenterWithTotalAssets(string dataCenterId, CancellationToken cancellationToken)
        {
            //var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, Constants.ServiceUris.Querystring.IncludeGroupLinksAndTotalAssets));
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, Constants.ServiceUris.Querystring.IncludeTotalAssets));
            var result = await ServiceInvoker.Invoke<DataCenter>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            result.Authentication = authentication;

            return result;
        }

        /// <summary>
        /// Gets the data center with the root group and all subgroups and with total assets.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenterWithGroupsAndTotalAssets(string dataCenterId)
        {
            return await GetDataCenterWithGroupsAndTotalAssets(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the data center with the root group and all subgroups and with total assets.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenterWithGroupsAndTotalAssets(string dataCenterId, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, Constants.ServiceUris.Querystring.IncludeGroupLinksAndTotalAssets));
            var result = await ServiceInvoker.Invoke<DataCenter>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            result.Authentication = authentication;

            return result;
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds)
        {

            return await GetRecentActivity(referenceIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, CancellationToken cancellationToken)
        {

            return await GetRecentActivity(referenceIds, 10, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordCountLimit)
        {

            return await GetRecentActivity(referenceIds, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> accounts, IEnumerable<string> referenceIds, int recordCountLimit)
        {
            return await GetRecentActivity(accounts, referenceIds, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <param name="referenceIds"></param>
        /// <param name="recordCountLimit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordCountLimit, CancellationToken cancellationToken)
        {
            var accounts = new List<string>();
            accounts.Add(authentication.AccountAlias);

            return await GetRecentActivity(accounts, referenceIds, recordCountLimit, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> accounts, IEnumerable<string> referenceIds, int recordCountLimit, CancellationToken cancellationToken)
        {
            var requestModel = new GetRecentActivityRequest() 
            { 
                EntityTypes = new List<string>(){ Constants.EntityTypes.Server, Constants.EntityTypes.Group },
                ReferenceIds = referenceIds,
                Accounts = accounts, 
                Limit = recordCountLimit 
            };

            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.DataCenter.GetRecentActivity, Configuration.BaseUri), requestModel);
            var result = await ServiceInvoker.Invoke<IEnumerable<Activity>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the deployment capabilities.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenterDeploymentCapability> GetDeploymentCapabilities(string dataCenterId)
        {
            return await GetDeploymentCapabilities(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the deployment capabilities.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenterDeploymentCapability> GetDeploymentCapabilities(string dataCenterId, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDeploymentCapabilities, Configuration.BaseUri, authentication.AccountAlias, dataCenterId));
            var result = await ServiceInvoker.Invoke<DataCenterDeploymentCapability>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
 
            return result;
        }

        /// <summary>
        /// Gets the information for a particular data center by accepting a hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DataCenter> GetDataCenterByLink(string uri)
        {
            return await GetDataCenterByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the information for a particular data center by accepting a hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<DataCenter> GetDataCenterByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<DataCenter>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            result.Authentication = authentication;

            return result;
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center by a hypermedia link. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DataCenterGroup> GetDataCenterGroupByLink(string uri)
        {
            return await GetDataCenterGroupByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center by a hypermedia link. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<DataCenterGroup> GetDataCenterGroupByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<DataCenterGroup>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            result.Authentication = authentication;

            return result;
        }
 
        /// <summary>
        /// Gets the compute limits.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<ComputeLimits> GetComputeLimitsByLink(string uri)
        {
            return await GetComputeLimitsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the compute limits.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<ComputeLimits> GetComputeLimitsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<ComputeLimits>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                result.MemoryGBFormatted = result.MemoryGB.Value.RoundNumberToNearestUpperLimit();
                result.StorageGBFormatted = result.StorageGB.Value.RoundNumberToNearestUpperLimit();
            }

            return result;
        }

        /// <summary>
        /// Gets the root group and all subgroups.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<Group> GetRootGroupByLink(string uri)
        {
            return await GetRootGroupByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the root group and all subgroups.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<Group> GetRootGroupByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<Group>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            result.Authentication = authentication;

            return result;
        }

        /// <summary>
        /// Gets the default settings.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DefaultSettings> GetDefaultSettingsByLink(string uri)
        {
            return await GetDefaultSettingsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the default settings.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<DefaultSettings> GetDefaultSettingsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<DefaultSettings>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the network limits.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<NetworkLimits> GetNetworkLimitsByLink(string uri)
        {
            return await GetNetworkLimitsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the network limits.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<NetworkLimits> GetNetworkLimitsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<NetworkLimits>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
