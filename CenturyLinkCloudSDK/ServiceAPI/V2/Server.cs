using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Server.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    public class Server: ServiceAPIBase
    {
        public async Task<GetServerResponse> GetServer(string accountAlias, string serverId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/servers/{0}/{1}", accountAlias, serverId),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var response = await Invoke<ServiceRequest, GetServerResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }

        public async Task<ServerPowerOpsResponse> PauseServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/pause", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var response = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }

        public async Task<ServerPowerOpsResponse> PowerOnServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/powerOn", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var response = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }

        public async Task<ServerPowerOpsResponse> PowerOffServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/powerOff", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var response = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }

        public async Task<ServerPowerOpsResponse> RebootServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/reboot", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var response = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }

        public async Task<ServerPowerOpsResponse> ShutDownServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/shutDown", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var response = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }

        public async Task<ServerPowerOpsResponse> ResetServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = "https://api.tier3.com/",
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/reset", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var response = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            return response;
        }
    }
}
