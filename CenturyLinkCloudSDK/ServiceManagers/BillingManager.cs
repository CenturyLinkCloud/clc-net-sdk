using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceManagers
{
    /// <summary>
    /// This class contains workflow methods regarding billing information. Workflow methods contain a series of service calls and logic to yield appropriate results for a complex operation.
    /// </summary>
    public class BillingManager: ServiceBase
    {
        internal BillingManager(Authentication authentication)
            : base(authentication)
        {
        }

        /// <summary>
        /// Gets the billing details for a list of datacenters.
        /// </summary>
        /// <param name="dataCenters"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetBillingDetails(IEnumerable<DataCenter> dataCenters)
        {
            return await GetBillingDetails(dataCenters, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details for a list of datacenters.
        /// </summary>
        /// <param name="dataCenters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetBillingDetails(IEnumerable<DataCenter> dataCenters, CancellationToken cancellationToken)
        {
            var dataCenterService = new DataCenterService(authentication);
            var rootHardwareGroupIds = new List<string>();

            foreach(var dataCenter in dataCenters)
            {
                var dataCenterGroup = await dataCenterService.GetDataCenterGroup(dataCenter.Id).ConfigureAwait(false);
                var rootGroup = await dataCenterGroup.GetRootHardwareGroup(cancellationToken).ConfigureAwait(false);

                rootHardwareGroupIds.Add(rootGroup.Id);
            }

            var billingDetail = await GetBillingDetails(rootHardwareGroupIds, cancellationToken).ConfigureAwait(false);
            return billingDetail;
        }

        /// <summary>
        /// Gets the billing details for a list of root hardware groups.
        /// </summary>
        /// <param name="rootHardwareGroupIds"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetBillingDetails(IEnumerable<string> rootHardwareGroupIds)
        {
            return await GetBillingDetails(rootHardwareGroupIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the billing details for a list of root hardware groups.
        /// </summary>
        /// <param name="rootHardwareGroupIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BillingDetail> GetBillingDetails(IEnumerable<string> rootHardwareGroupIds, CancellationToken cancellationToken)
        {
            var groupService = new GroupService(authentication);
            var billingDetail = new BillingDetail();

            foreach (var groupId in rootHardwareGroupIds)
            {
                var groupBillingDetail = await groupService.GetGroupBillingDetails(groupId).ConfigureAwait(false);

                foreach (var group in groupBillingDetail.Groups)
                {
                    foreach(var server in group.Value.Servers)
                    {
                        billingDetail.MonthlyEstimate += server.Value.MonthlyEstimate;
                        billingDetail.CurrentHour += server.Value.CurrentHour;
                        billingDetail.MonthToDate += server.Value.MonthToDate;
                    }
                }
            }

            return billingDetail;
        }
    }
}
