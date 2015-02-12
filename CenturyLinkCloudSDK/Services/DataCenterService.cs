using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.DataCenters;
using CenturyLinkCloudSDK.Runtime;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with data centers.
    /// </summary>
    public class DataCenterService
    {
        private AuthenticationInfo userAuthentication;

        internal DataCenterService(AuthenticationInfo userAuthentication)
        {
            this.userAuthentication = userAuthentication;
        }

        /// <summary>
        /// Gets the list of data centers that a given account has access to. 
        /// Use this operation when you need the list of data center names and codes that you have access to. 
        /// Using that list of data centers, you can then query for the root group, and all the child groups in an entire data center.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <returns>An asynchronous Task of IEnumerable of DataCenter.</returns>
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
        /// <returns>An asynchronous Task of IEnumerable of DataCenter.</returns>
        public async Task<IReadOnlyList<DataCenter>> GetDataCenters(CancellationToken cancellationToken)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenters, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetDataCentersResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

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
        /// <returns>An asynchronous Task of DataCenter.</returns>
        public async Task<DataCenter> GetDataCenter(string dataCenter)
        {
            return await GetDataCenter(dataCenter, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the information for a particular data center.
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of DataCenter.</returns>
        public async Task<DataCenter> GetDataCenter(string dataCenter, CancellationToken cancellationToken)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenter, userAuthentication.AccountAlias, dataCenter),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetDataCenterResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

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
        /// <returns>An asynchronous Task of DataCenterGroup</returns>
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
        /// <returns>An asynchronous Task of DataCenterGroup</returns>
        public async Task<DataCenterGroup> GetDataCenterGroup(string dataCenter, CancellationToken cancellationToken)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.DataCenter.GetDataCenterGroup, userAuthentication.AccountAlias, dataCenter),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetDataCenterGroupsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

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
        /// <param name="hypermediaLink"></param>
        /// <returns>An asynchronous Task of DataCenter.</returns>
        public async Task<DataCenter> GetDataCenterByHyperMediaLink(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = Constants.ServiceUris.ApiBaseAddress + hypermediaLink,
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetDataCenterResponse>(serviceRequest, CancellationToken.None).ConfigureAwait(false);

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
        /// <param name="hypermediaLink"></param>
        /// <returns>An asynchronous Task of DataCenterGroup</returns>
        public async Task<DataCenterGroup> GetDataCenterGroupByHyperMediaLink(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = Constants.ServiceUris.ApiBaseAddress + hypermediaLink + Constants.ServiceUris.Querystring.IncludeGroupLinks,
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetDataCenterGroupsResponse>(serviceRequest, CancellationToken.None).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }
    }
}
