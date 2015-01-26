using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Queues.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    /// <summary>
    /// This class contains operations associated with queues.
    /// </summary>
    public class QueueService : ServiceBase
    {
        /// <summary>
        /// Gets the status of a particular job in the queue, which keeps track of any long-running 
        /// asynchronous requests (such as server power operations or provisioning tasks).
        /// Use this API operation when you want to check the status of a specific job in the queue. 
        /// It is usually called after running a batch job and receiving the job identifier from the status link (e.g. Power On, Create Server, etc.) 
        /// and will typically continue to get called until a "succeeded" or "failed" response is returned.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="statusId"></param>
        /// <returns>An asynchronous Task of Queue.</returns>
        public async Task<Queue> GetStatus(string accountAlias, string statusId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/status/{1}", accountAlias, statusId),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetStatusResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Queue;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the status of a particular job in the queue, which keeps track of any long-running 
        /// asynchronous requests (such as server power operations or provisioning tasks) by hypermedia link.
        /// Use this API operation when you want to check the status of a specific job in the queue. 
        /// It is usually called after running a batch job and receiving the job identifier from the status link (e.g. Power On, Create Server, etc.) 
        /// and will typically continue to get called until a "succeeded" or "failed" response is returned.
        /// </summary>
        /// <param name="hypermediaLink"></param>
        /// <returns>An asynchronous Task of Queue.</returns>
        public async Task<Queue> GetStatus(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = Constants.API_BASE_ADDRESS + hypermediaLink,
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetStatusResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Queue;
                return response;
            }

            return null;
        }
    }
}
