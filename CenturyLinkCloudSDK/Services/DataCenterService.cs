using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
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
        public async Task<IEnumerable<DataCenter>> GetDataCenters()
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
        public async Task<IEnumerable<DataCenter>> GetDataCenters(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenters, Configuration.BaseUri, authentication.AccountAlias, string.Empty));
            var result = await ServiceInvoker.Invoke<IEnumerable<DataCenter>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenter(string dataCenterId)
        {
            return await GetDataCenter(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenter(string dataCenterId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, string.Empty);
            return await GetDataCenterByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenterId)
        {
            return await GetDataCenterGroup(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenterId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, Constants.ServiceUris.Querystring.IncludeGroupLinks);
            return await GetDataCenterGroupByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///  Gets the total assets for all data centers.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataCenter>> GetAllDataCentersWithTotalAssets()
        {
            return await GetAllDataCentersWithTotalAssets(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the total assets for all data centers.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataCenter>> GetAllDataCentersWithTotalAssets(CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenters, Configuration.BaseUri, authentication.AccountAlias, Constants.ServiceUris.Querystring.IncludeTotalAssets));
            var result = await ServiceInvoker.Invoke<IEnumerable<DataCenter>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets a data center with total asset information.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenterWithTotalAssets(string dataCenterId)
        {
            return await GetDataCenterWithTotalAssets(dataCenterId, CancellationToken.None).ConfigureAwait(false);
        }


        /// <summary>
        /// Gets a data center with total asset information.
        /// </summary>
        /// <param name="dataCenterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataCenter> GetDataCenterWithTotalAssets(string dataCenterId, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, Configuration.BaseUri, authentication.AccountAlias, dataCenterId, Constants.ServiceUris.Querystring.IncludeTotalAssets));
            var result = await ServiceInvoker.Invoke<DataCenter>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the information for a particular data center by accepting a hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DataCenter> GetDataCenterByLink(string uri)
        {
            return await GetDataCenterByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the information for a particular data center by accepting a hypermedia link.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<DataCenter> GetDataCenterByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<DataCenter>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center by a hypermedia link. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<DataCenterGroup> GetDataCenterGroupByLink(string uri)
        {
            return await GetDataCenterGroupByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the full hierarchy of groups that exist within a particular account and data center by a hypermedia link. 
        /// Use this operation when you want to discover the name of the root hardware group for a data center. 
        /// Once you have that group alias, you can issue a secondary query to retrieve the entire group hierarchy for a given data center.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<DataCenterGroup> GetDataCenterGroupByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<DataCenterGroup>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            result.Authentication = authentication;

            return result;
        }
    }
}
