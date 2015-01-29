using CenturyLinkCloudSDK.ServiceModels.Common;
using CenturyLinkCloudSDK.ServiceModels.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels.Queues.Responses
{
    /// <summary>
    /// This class contains the response from the GetStatus operation.
    /// </summary>
    internal class GetStatusResponse : IServiceResponse
    {
        private Queue response = new Queue();

        public object Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as Queue;
            }
        }
    }
}
