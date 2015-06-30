using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
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
        internal QueueService(Authentication authentication, IServiceInvoker serviceInvoker)
            : base(authentication, serviceInvoker)
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
        /// <param name="statusId">The id of the job</param>
        /// <returns>The queue status</returns>
        public Task<Queue> GetStatus(string statusId)
        {
            return GetStatus(statusId, CancellationToken.None);
        }

        /// <summary>
        /// Gets the status of a particular job in the queue, which keeps track of any long-running 
        /// asynchronous requests (such as server power operations or provisioning tasks).
        /// Use this API operation when you want to check the status of a specific job in the queue. 
        /// It is usually called after running a batch job and receiving the job identifier from the status link (e.g. Power On, Create Server, etc.) 
        /// and will typically continue to get called until a "succeeded" or "failed" response is returned.
        /// </summary>
        /// <param name="statusId">The id of the job</param>        
        /// <returns>The queue status</returns>
        public Task<Queue> GetStatus(string statusId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Queue.GetStatus, Configuration.BaseUri, authentication.AccountAlias, statusId);
            return GetStatusByLink(uri, cancellationToken);
        }

        internal async Task<Queue> GetStatusByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return await serviceInvoker.Invoke<Queue>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }
    }
}
