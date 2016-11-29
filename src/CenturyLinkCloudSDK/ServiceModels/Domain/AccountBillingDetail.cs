using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class AccountBillingDetail
    {
        public DateTime Date { get; set; }

        public string AccountAlias { get; set; }

        public BillingDetail Total { get; set; }
    }
}
