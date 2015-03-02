﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class AccountActivity
    {
        public string AccountAlias { get; set; }

        public string Body { get; set; }

        public string AccountDescription { get; set; }

        public int EntityId { get; set; }

        public string ReferenceId { get; set; }

        public string EntityType { get; set; }

        public string LocationAlias { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Subject { get; set; }
    }
}
