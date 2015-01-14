using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses
{
    public class GetServerResponse: IServiceResponseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GroupId { get; set; }

        public bool IsTemplate { get; set; }

        public string LocationId { get; set; }

        public string OsType { get; set; }

        public string Status { get; set; }

        public ServerDetail Details { get; set; }

        public string Type { get; set; }

        public string StorageType { get; set; }

        public ChangeInfo ChangeInfo { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }
}
