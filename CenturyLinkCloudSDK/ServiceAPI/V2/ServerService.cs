using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceModels.V2.Common;
using CenturyLinkCloudSDK.ServiceModels.V2.Servers.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceAPI.V2
{
    /// <summary>
    /// This class contains operations associated with servers.
    /// </summary>
    public class ServerService: ServiceBase
    {
        /// <summary>
        /// Gets the details for a individual server.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverId"></param>
        /// <returns>An asynchronous Task of Server.</returns>
        public async Task<Server> GetServer(string accountAlias, string serverId)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/servers/{0}/{1}", accountAlias, serverId),
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetServerResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Server;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the details for a individual server by hypermedia link.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources. 
        /// </summary>
        /// <param name="hypermediaLink"></param>
        /// <returns>An asynchronous Task of Server.</returns>
        public async Task<Server> GetServer(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = Constants.API_BASE_ADDRESS + hypermediaLink,
                MediaType = "application/json",
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await Invoke<ServiceRequest, GetServerResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as Server;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Sends the pause operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to pause a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the pause command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> PauseServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/pause", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<ServerOperation>;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Sends the power on operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to power on a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the power on command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> PowerOnServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/powerOn", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<ServerOperation>;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Sends the power off operation to a list of servers and adds operation to queue. 
        /// Use this operation when you want to power off a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the power off command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> PowerOffServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/powerOff", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<ServerOperation>;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Sends the reboot operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to reboot a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the reboot command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> RebootServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/reboot", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<ServerOperation>;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Sends the shut down operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to shut down a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the shut down command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> ShutDownServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/shutDown", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<ServerOperation>;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Sends the reset operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to reset a single server or group of servers. 
        /// It should be used in conjunction with the  Queue GetStatus operation to check the result of the reset command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> ResetServer(string accountAlias, List<string> serverIds)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                BaseAddress = Constants.API_BASE_ADDRESS,
                ServiceUri = string.Format("https://api.tier3.com/v2/operations/{0}/servers/reset", accountAlias),
                MediaType = "application/json",
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response as IEnumerable<ServerOperation>;
                return response;
            }

            return null;
        }
    }
}
