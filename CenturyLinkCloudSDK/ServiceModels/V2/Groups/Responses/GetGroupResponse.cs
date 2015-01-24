using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.V2.Groups.Responses
{
    /// <summary>
    /// This class contains the response from the GetGroup operation.
    /// </summary>
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
