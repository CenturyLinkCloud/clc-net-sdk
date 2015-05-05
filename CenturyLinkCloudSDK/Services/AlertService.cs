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
        /// Gets the alerts for a particular server.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="alertPolicies"></param>
        /// <param name="statistics"></param>
        /// <returns></returns>
        public async Task<List<Alert>> GetServerAlerts(string serverId, IEnumerable<AlertPolicy> alertPolicies, Statistics statistics)
        {
            return await GetServerAlerts(serverId, alertPolicies, statistics, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the alerts for a particular server.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="alertPolicies"></param>
        /// <param name="statistics"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Alert>> GetServerAlerts(string serverId, IEnumerable<AlertPolicy> alertPolicies, Statistics statistics, CancellationToken cancellationToken)
        {
            var alerts = await ScanAlertPoliciesForAlerts(serverId, alertPolicies, statistics).ConfigureAwait(false);
            return alerts;
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
                                    var alert = GetServerAlert(alertPolicy, trigger, statistic, server.Id);

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
        /// Gets the triggers for an alert policy.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<IEnumerable<AlertTrigger>> GetTriggersByAlertPolicyLink(string uri)
        {
            return await GetTriggersByAlertPolicyLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the triggers for an alert policy.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<IEnumerable<AlertTrigger>> GetTriggersByAlertPolicyLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var alertPolicy = await ServiceInvoker.Invoke<AlertPolicy>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return alertPolicy.Triggers;
        }

        /// <summary>
        /// Loops through alert policies to determine if a server needs to be alerted.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="alertPolicies"></param>
        /// <param name="statistics"></param>
        /// <returns></returns>
        private async Task<List<Alert>> ScanAlertPoliciesForAlerts(string serverId, IEnumerable<AlertPolicy> alertPolicies, Statistics statistics)
        {
            var alerts = new List<Alert>();

            foreach (var alertPolicy in alertPolicies)
            {
                var statistic = statistics.Stats.FirstOrDefault();

                if (statistic != null)
                {
                    alertPolicy.Authentication = authentication;
                    var triggers = await alertPolicy.GetTriggers().ConfigureAwait(false);

                    if (triggers != null)
                    {
                        foreach (var trigger in triggers)
                        {
                            try
                            {
                                var alert = GetServerAlert(alertPolicy, trigger, statistic, serverId);

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
        private Alert GetServerAlert(AlertPolicy alertPolicy, AlertTrigger trigger, ServerStatistic statistic, string serverId)
        {
            if (string.Equals(trigger.Metric, Constants.Metrics.Cpu, StringComparison.CurrentCultureIgnoreCase))
            {
                if (statistic.CpuPercent > (trigger.Threshold / 100f))
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, serverId, Constants.AlertMessages.CpuAlert);
                }
            }

            if (string.Equals(trigger.Metric, Constants.Metrics.Memory, StringComparison.CurrentCultureIgnoreCase))
            {
                if (statistic.MemoryPercent > (trigger.Threshold / 100f))
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, serverId, Constants.AlertMessages.MemoryAlert);
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

                if (totalDiskUsage > (trigger.Threshold / 100f))
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, serverId, Constants.AlertMessages.DiskUsageAlert);
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
        private Alert GenerateAlert(AlertPolicy alertPolicy, AlertTrigger trigger, ServerStatistic statistic, string serverId, string message)
        {
            var alert = new Alert()
            {
                AlertPolicyId = alertPolicy.Id,
                ServerId = serverId,
                Metric = trigger.Metric,
                Message = message,
                GeneratedOn = statistic.Timestamp
            };

            return alert;
        }
    }
}
