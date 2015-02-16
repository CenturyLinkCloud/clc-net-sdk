using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.Queues;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with queues.
    /// </summary>
    public class QueueService : ServiceBase
    {
        internal QueueService(Authentication authentication)
            : base(authentication)
        {
            this.authentication = authentication;
        }

        /// <summary>
        /// Gets the status of a particular job in the queue, which keeps track of any long-running 
        /// asynchronous requests (such as server power operations or provisioning tasks).
        /// Use this API operation when you want to check the status of a specific job in the queue. 
        /// It is usually called after running a batch job and receiving the job identifier from the status link (e.g. Power On, Create Server, etc.) 
        /// and will typically continue to get called until a "succeeded" or "failed" response is returned.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public async Task<Queue> GetStatus(string statusId)
        {
            return await GetStatus(statusId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the status of a particular job in the queue, which keeps track of any long-running 
        /// asynchronous requests (such as server power operations or provisioning tasks).
        /// Use this API operation when you want to check the status of a specific job in the queue. 
        /// It is usually called after running a batch job and receiving the job identifier from the status link (e.g. Power On, Create Server, etc.) 
        /// and will typically continue to get called until a "succeeded" or "failed" response is returned.
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of Queue.</returns>
        public async Task<Queue> GetStatus(string statusId, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Queue.GetStatus, authentication.AccountAlias, statusId), string.Empty);
            var result = await ServiceInvoker.Invoke<GetStatusResponse>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        /// <returns></returns>
        internal async Task<Queue> GetStatusByLink(string uri)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri, string.Empty);
            var result = await ServiceInvoker.Invoke<GetStatusResponse>(httpRequestMessage, CancellationToken.None).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }
    }
}
