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
        internal AccountService(Authentication authentication)
            : base(authentication)
        {

        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <param name="recordCountLimit"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountActivity>> GetRecentActivity()
        {
            return await GetRecentActivity(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountActivity>> GetRecentActivity(CancellationToken cancellationToken)
        {
            var accounts = new List<String>();
            accounts.Add(authentication.AccountAlias);

            return await GetRecentActivity(accounts, 10, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountActivity>> GetRecentActivity(int recordCountLimit)
        {
            return await GetRecentActivity(recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountActivity>> GetRecentActivity(int recordCountLimit, CancellationToken cancellationToken)
        {
            var accounts = new List<String>();
            accounts.Add(authentication.AccountAlias);

            return await GetRecentActivity(accounts, recordCountLimit, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountActivity>> GetRecentActivity(IEnumerable<string> accounts, int recordCountLimit)
        {
            return await GetRecentActivity(accounts, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent account activity.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountActivity>> GetRecentActivity(IEnumerable<string> accounts, int recordCountLimit, CancellationToken cancellationToken)
        {
            var requestModel = new GetRecentActivityRequest() { Accounts = accounts, Limit = recordCountLimit };

            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Account.GetRecentActivity, Configuration.BaseUri), requestModel);
            var result = await ServiceInvoker.Invoke<IEnumerable<AccountActivity>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the account total assets.
        /// </summary>
        /// <param name="dataCenterIds"></param>
        /// <returns></returns>
        public async Task<TotalAssets> GetAccountTotalAssets(IEnumerable<string> dataCenterIds)
        {
            return await GetAccountTotalAssets(dataCenterIds, CancellationToken.None).ConfigureAwait(false);
        }


        /// <summary>
        /// Gets the account total assets.
        /// </summary>
        /// <param name="dataCenterIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TotalAssets> GetAccountTotalAssets(IEnumerable<string> dataCenterIds, CancellationToken cancellationToken)
        {
            var totalAssets = new TotalAssets();
            var tasks = new List<Task<DataCenter>>();
            var dataCenterService = new DataCenterService(authentication);

            foreach (var dataCenterId in dataCenterIds)
            {
                //tasks.Add(Task.Run(() => dataCenterService.GetDataCenterWithTotalAssets(dataCenterId, cancellationToken).Result));
                tasks.Add(Task.Run(async () => await dataCenterService.GetDataCenterWithTotalAssets(dataCenterId, cancellationToken).ConfigureAwait(false)));
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                totalAssets.Servers += task.Result.Totals.Servers;
                totalAssets.Cpus += task.Result.Totals.Cpus;
                totalAssets.MemoryGB += task.Result.Totals.MemoryGB;
                totalAssets.StorageGB += task.Result.Totals.StorageGB;
                totalAssets.Queue += task.Result.Totals.Queue;
            }

            return totalAssets;
        }
    }
}
