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
    /// This class contains alert operations.
    /// </summary>
    public class AlertService : ServiceBase
    {
        internal AlertService(Authentication authentication)
            : base(authentication)
        {

        }

        /// <summary>
        /// Gets the account alert policies.
        /// </summary>
        /// <returns></returns>
        public async Task<AlertPolicyItems> GetAlertPoliciesForAccount()
        {
            return await GetAlertPoliciesForAccount(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the account alert policies.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AlertPolicyItems> GetAlertPoliciesForAccount(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Alerts.GetAlertPoliciesForAccount, Configuration.BaseUri, authentication.AccountAlias));
            var result = await ServiceInvoker.Invoke<AlertPolicyItems>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
