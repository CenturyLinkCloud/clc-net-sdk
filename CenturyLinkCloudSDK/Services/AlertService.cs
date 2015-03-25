using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<AlertPolicies> GetAlertPolicies()
        {
            return await GetAlertPolicies(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the account alert policies.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AlertPolicies> GetAlertPolicies(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Alerts.GetAlertPoliciesForAccount, Configuration.BaseUri, authentication.AccountAlias));
            var result = await ServiceInvoker.Invoke<AlertPolicies>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the alerts for all servers in the account that subscribe to an alert policy.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Alert>> GetServerAlerts()
        {
            return await GetServerAlerts(CancellationToken.None).ConfigureAwait(false);
        }

       /// <summary>
        /// Gets the alerts for all servers in the account that subscribe to an alert policy.
       /// </summary>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
        public async Task<IEnumerable<Alert>> GetServerAlerts(CancellationToken cancellationToken)
        {
            var alerts = new List<Alert>();

            var alertPolicies = await GetAlertPolicies(cancellationToken).ConfigureAwait(false);

            foreach (var alertPolicy in alertPolicies.Items)
            {
                alertPolicy.Authentication = authentication;

                var servers = await alertPolicy.GetServers().ConfigureAwait(false);

                foreach (var server in servers)
                {
                    server.Authentication = authentication;

                    var statistics = await server.GetStatistics().ConfigureAwait(false);

                    if (statistics != null)
                    {
                        var statistic = statistics.Stats.FirstOrDefault();

                        if (statistic != null)
                        {
                            foreach (var trigger in alertPolicy.Triggers)
                            {
                                try
                                {
                                    var alert = GetServerAlert(alertPolicy, trigger, statistic, server);

                                    if (alert != null)
                                    {
                                        alerts.Add(alert);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    var exception = new CenturyLinkCloudServiceException(Constants.ExceptionMessages.AlertGenerationExceptionMessage, ex);
                                    throw exception;
                                }
                            }
                        }
                    }
                }
            }

            return alerts;
        }

        /// <summary>
        /// Determines if a server has violated a certain alert policy and returns an alert if true;
        /// </summary>
        /// <param name="alertPolicy"></param>
        /// <param name="trigger"></param>
        /// <param name="statistic"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        private Alert GetServerAlert(AlertPolicy alertPolicy, AlertTrigger trigger, ServerStatistic statistic, Server server)
        {
            if (string.Equals(trigger.Metric, Constants.Metrics.Cpu, StringComparison.CurrentCultureIgnoreCase))
            {
                if (statistic.CpuPercent > trigger.Threshold / 100)
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, server, Constants.AlertMessages.CpuAlert);
                }
            }

            if (string.Equals(trigger.Metric, Constants.Metrics.Memory, StringComparison.CurrentCultureIgnoreCase))
            {
                if (statistic.MemoryPercent > trigger.Threshold / 100)
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, server, Constants.AlertMessages.MemoryAlert);
                }
            }

            if (string.Equals(trigger.Metric, Constants.Metrics.Disk, StringComparison.CurrentCultureIgnoreCase))
            {
                float totalDiskUsage = 0;

                foreach (var guestDiskUsage in statistic.GuestDiskUsage)
                {
                    float usage = (guestDiskUsage.ConsumedMB / guestDiskUsage.CapacityMB);
                    totalDiskUsage += usage;
                }

                if (totalDiskUsage > trigger.Threshold / 100)
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, server, Constants.AlertMessages.DiskUsageAlert);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates an alert.
        /// </summary>
        /// <param name="alertPolicy"></param>
        /// <param name="trigger"></param>
        /// <param name="statistic"></param>
        /// <param name="server"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private Alert GenerateAlert(AlertPolicy alertPolicy, AlertTrigger trigger, ServerStatistic statistic, Server server, string message)
        {
            var alert = new Alert()
            {
                AlertPolicyId = alertPolicy.Id,
                ServerId = server.Id,
                Metric = trigger.Metric,
                Message = message,
                GeneratedOn = statistic.Timestamp
            };

            return alert;
        }
    }
}
