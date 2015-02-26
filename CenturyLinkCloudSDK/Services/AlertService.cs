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
        public async Task<AlertPolicies> GetAlertPoliciesForAccount()
        {
            return await GetAlertPoliciesForAccount(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the account alert policies.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AlertPolicies> GetAlertPoliciesForAccount(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Alerts.GetAlertPoliciesForAccount, Configuration.BaseUri, authentication.AccountAlias));
            var result = await ServiceInvoker.Invoke<AlertPolicies>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the alerts for all servers in the account.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetServerAlerts(CancellationToken cancellationToken)
        {
            var alerts = new List<string>();

            var alertPolicies = await GetAlertPoliciesForAccount(cancellationToken).ConfigureAwait(false);

            foreach (var alertPolicy in alertPolicies.Items)
            {
                alertPolicy.Authentication = authentication;

                var servers = await alertPolicy.GetServers().ConfigureAwait(false);

                foreach (var server in servers)
                {
                    server.Authentication = authentication;

                    var statistics = await server.GetStatistics().ConfigureAwait(false);
                    var statistic = statistics.Stats.FirstOrDefault();

                    foreach (var trigger in alertPolicy.Triggers)
                    {
                        if (string.Equals(trigger.Metric, "cpu", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (statistic != null)
                            {
                                if (statistic.Cpu > trigger.Threshold)
                                {
                                    alerts.Add(server.Id);
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
