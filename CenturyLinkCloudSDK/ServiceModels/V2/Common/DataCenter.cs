﻿using System;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Common
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class DataCenter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }
}
