using System;
using System.Collections.Generic;
using System.Linq;

namespace CenturyLinkCloudSDK.ServiceModels
{
    internal class GroupBillingDetail
    {
        public DateTime Date { get; set; }
        public Dictionary<string, GroupBilling> Groups { get; set; }

        public BillingDetail GetTotals()
        {
            return
                Groups
                    .SelectMany(g => g.Value.Servers)
                    .Select(s => s.Value)
                    .Aggregate(
                        new BillingDetail(),
                        (acc, s) =>
                        {
                            acc.MonthlyEstimate += s.MonthlyEstimate;
                            acc.CurrentHour += s.CurrentHour;
                            acc.MonthToDate += s.MonthToDate;
                            acc.OneTimeCharges += s.OneTimeCharges;
                            acc.TemplateCost += s.TemplateCost;
                            return acc;
                        });
        }
    }
}
