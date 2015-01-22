using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Groups.Responses
{
    internal class GetGroupResponse: IServiceResponse
    {
        private Group response = new Group();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as Group;
            }
        }
    }
}
