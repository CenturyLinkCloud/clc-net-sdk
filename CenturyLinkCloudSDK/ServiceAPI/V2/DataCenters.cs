using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.DataCenters.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class DataCenters: ServiceAPIBase
    {
        public async Task<IEnumerable<DataCenter>> GetDataCenters(string accountAlias)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/datacenters/{0}", accountAlias),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetDataCentersResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<DataCenter>;
                return response;
            }

            return null;
        }

        public async Task<DataCenter> GetDataCenter(string accountAlias, string dataCenter)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/datacenters/{0}/{1}", accountAlias, dataCenter),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetDataCenterResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as DataCenter;
                return response;
            }

            return null;
        }

        public async Task<DataCenter> GetDataCenter(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = hypermediaLink,
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetDataCenterResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as DataCenter;
                return response;
            }

            return null;
        }

        public async Task<DataCenterGroup> GetDataCenterGroup(string accountAlias, string dataCenter)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/datacenters/{0}/{1}?groupLinks=true", accountAlias, dataCenter),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetDataCenterGroupsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as DataCenterGroup;
                return response;
            }

            return null;
        }

        public async Task<DataCenterGroup> GetDataCenterGroup(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format(hypermediaLink + "?groupLinks=true"),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetDataCenterGroupsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as DataCenterGroup;
                return response;
            }

            return null;
        }
    }
}
