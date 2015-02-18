using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.DataCenters;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with data centers.
    /// </summary>
    public class DataCenterService : ServiceBase
    {
        internal DataCenterService(Authentication authentication)
            : base(authentication)
        {

        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// Use this operation when you need the list of data center names and codes that you have access to. 
        /// Using that list of data centers, you can then query for the root group, and all the child groups in an entire data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<DataCenter>> GetDataCenters()
        {
            return await GetDataCenters(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// Use this operation when you need the list of data center names and codes that you have access to. 
        /// Using that list of data centers, you can then query for the root group, and all the child groups in an entire data center.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<DataCenter>> GetDataCenters(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenters, Configuration.BaseUri, authentication.AccountAlias));
            var result = await ServiceInvoker.Invoke<GetDataCentersResponse>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="dataCenter"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenter(string dataCenter)
        {
            return await GetDataCenter(dataCenter, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenter(string dataCenter, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenter));
            var result = await ServiceInvoker.Invoke<GetDataCenterResponse>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            var uri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenter);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="dataCenter"></param>
        /// <returns></returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenter)
        {
            return await GetDataCenterGroup(dataCenter, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenter, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenterGroup, Configuration.BaseUri, authentication.AccountAlias, dataCenter));
            var result = await ServiceInvoker.Invoke<GetDataCenterGroupsResponse>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            
            result.Response.Authentication = authentication;

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the information for a particular data center by accepting a hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DataCenter> GetDataCenterByLink(string uri)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri, string.Empty);
            var result = await ServiceInvoker.Invoke<GetDataCenterResponse>(httpRequestMessage, CancellationToken.None).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center by a hypermedia link. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DataCenterGroup> GetDataCenterGroupByHyperMediaLink(string uri)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri + Constants.ServiceUris.Querystring.IncludeGroupLinks);
            var result = await ServiceInvoker.Invoke<GetDataCenterGroupsResponse>(httpRequestMessage, CancellationToken.None).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }
    }
}
