﻿using CenturyLinkCloudSDK.Extensions;
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
        ServerService serverService;
        internal AlertService(Authentication authentication, IServiceInvoker serviceInvoker, ServerService serverService)
            : base(authentication, serviceInvoker)
        {
            this.serverService = serverService;
        }

        void SetInternalAlertPolicyProperties(AlertPolicy policy)
        {
            policy.ServerService = serverService;
        }
        
        /// <summary>
        /// Gets the alerts for all servers in the account that subscribe to an alert policy.
        /// </summary>
        /// <returns>All alerts</returns>
        public Task<IEnumerable<Alert>> GetAllServerAlerts()
        {
            return GetAllServerAlerts(CancellationToken.None);
        }

        /// <summary>
        /// Gets the alerts for all servers in the account that subscribe to an alert policy.
        /// </summary>        
        /// <returns>All alerts</returns>
        public async Task<IEnumerable<Alert>> GetAllServerAlerts(CancellationToken cancellationToken)
        {
            var alerts = new List<Alert>();

            var alertPolicies = await GetAlertPoliciesByLink(cancellationToken).ConfigureAwait(false);

            var policyAndServers =
                await alertPolicies.Items
                    .SelectEachAsync(
                        async a =>
                        {
                            var s = await a.GetServers().ConfigureAwait(false);
                            return new { Policy = a, Servers = s };
                        }, cancellationToken)
                    .ConfigureAwait(false);

            var statsForServer =
                await policyAndServers
                    .SelectMany(p => p.Servers)
                    .Distinct(new Server.ByIdEqualityComparer())
                    .SelectEachAsync(
                        async s =>
                        {
                            var stat = await s.GetStatistics().ConfigureAwait(false);
                            return new { ServerId = s.Id, Stats = stat };
                        }, cancellationToken)
                    .ConfigureAwait(false);

            var statsByServer = statsForServer.ToDictionary(s => s.ServerId, s => s.Stats);      
            
            foreach(var alertPolicy in policyAndServers)
            {
                foreach(var server in alertPolicy.Servers)
                {
                    if(statsByServer.ContainsKey(server.Id) && (statsByServer[server.Id] != null))
                    {
                        var statistic = statsByServer[server.Id].Stats.FirstOrDefault();
                        if(statistic != null)
                        {
                            foreach(var trigger in alertPolicy.Policy.Triggers)
                            {
                                try
                                {
                                    var alert = GetServerAlert(alertPolicy.Policy, trigger, statistic, server.Id);

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
        /// Gets the account alert policies.
        /// </summary>
        /// <returns>The alerty policies for this account</returns>
        public Task<AlertPolicies> GetAlertPolicies()
        {
            return GetAlertPolicies(CancellationToken.None);
        }

        /// <summary>
        /// Gets the account alert policies.
        /// </summary>
        /// <returns>The alerty policies for this account</returns>
        public Task<AlertPolicies> GetAlertPolicies(CancellationToken cancellationToken)
        {
            return GetAlertPoliciesByLink(cancellationToken);
        }

        internal async Task<AlertPolicies> GetAlertPoliciesByLink(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Alerts.GetAlertPoliciesForAccount, Configuration.BaseUri, authentication.AccountAlias));
            var result = await serviceInvoker.Invoke<AlertPolicies>(httpRequestMessage, cancellationToken).ConfigureAwait(false);            
            if(result.Items != null)
            {
                foreach (var p in result.Items) { SetInternalAlertPolicyProperties(p); }
            }
            return result;
        }

        /// <summary>
        /// Determines if a server has violated a certain alert policy and returns an alert if true;
        /// </summary>
        private Alert GetServerAlert(AlertPolicy alertPolicy, AlertTrigger trigger, ServerStatistic statistic, string serverId)
        {
            if (string.Equals(trigger.Metric, Constants.Metrics.Cpu, StringComparison.CurrentCultureIgnoreCase))
            {
                if (statistic.CpuPercent > ((float)trigger.Threshold))
                {
                    return GenerateAlert(alertPolicy, trigger, statistic, serverId, Constants.AlertMessages.CpuAlert);
                }
            }

            if (string.Equals(trigger.Metric, Constants.Metrics.Memory, StringComparison.CurrentCultureIgnoreCase))
            {
                if (statistic.MemoryPercent > ((float)trigger.Threshold))
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

        /// <summary>
        /// Gets the alerts for a particular server.
        /// </summary>
        /// <param name="server">The server</param>
        /// <returns>The alerts for the indicated server</returns>
        public Task<List<Alert>> GetServerAlerts(Server server)
        {
            return GetServerAlerts(server, CancellationToken.None);
        }

        /// <summary>
        /// Gets the alerts for a particular server.
        /// </summary>
        /// <param name="server">The server</param>
        /// <returns>The alerts for the indicated server</returns>
        public async Task<List<Alert>> GetServerAlerts(Server server, CancellationToken cancellationToken)
        {
            var statisticsTask = server.GetStatistics(cancellationToken);
            var alertPoliciesTasks =
                server
                    .Details
                    .AlertPolicies
                    .Select(
                        a =>
                            GetAlertPolicyByLink(string.Format("{0}{1}", Configuration.BaseUri, a.selfLink.Value.Href), cancellationToken));
            await Task.WhenAll(alertPoliciesTasks).ConfigureAwait(false);
            var statistics = await statisticsTask.ConfigureAwait(false);
            var alertPolicies = alertPoliciesTasks.Select(a => a.Result);

            return ScanAlertPoliciesForAlerts(server, alertPolicies, statistics);            
        }

        async Task<AlertPolicy> GetAlertPolicyByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<AlertPolicy>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            if (result != null)
            {
                SetInternalAlertPolicyProperties(result);
            }
            return result;
        }

        /// <summary>
        /// Loops through alert policies to determine if a server needs to be alerted.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="alertPolicies"></param>
        /// <param name="statistics"></param>
        /// <returns></returns>
        private List<Alert> ScanAlertPoliciesForAlerts(Server server, IEnumerable<AlertPolicy> alertPolicies, Statistics statistics)
        {
            var alerts = new List<Alert>();

            foreach (var alertPolicy in alertPolicies)
            {
                var statistic = statistics.Stats.FirstOrDefault();

                if (statistic != null)
                {
                    var triggers = alertPolicy.Triggers;
                    if (triggers != null)
                    {
                        foreach (var trigger in triggers)
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

            return alerts;
        }        
    }
}
