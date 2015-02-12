using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using CenturyLinkCloudSDK.Runtime;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with servers.
    /// </summary>
    public class ServerService
    {
        private AuthenticationInfo userAuthentication;

        internal ServerService(AuthenticationInfo userAuthentication)
        {
            this.userAuthentication = userAuthentication;
        }

        /// <summary>
        /// Gets the details for a individual server.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverId"></param>
        /// <returns>An asynchronous Task of Server.</returns>
        public async Task<Server> GetServer(string serverId)
        {
            return await GetServer(serverId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the details for a individual server.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of Server.</returns>
        public async Task<Server> GetServer(string serverId, CancellationToken cancellationToken)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.GetServer, userAuthentication.AccountAlias, serverId),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetServerResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<Server> GetServerByHypermediaLink(string hypermediaLink)
        {
            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = Constants.ServiceUris.ApiBaseAddress + hypermediaLink,
                BearerToken = userAuthentication.BearerToken,
                RequestModel = null,
                HttpMethod = HttpMethod.Get
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, GetServerResponse>(serviceRequest, CancellationToken.None).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<IReadOnlyList<ServerOperation>> PauseServer(List<string> serverIds)
        {
            return await PauseServer(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the pause operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to pause a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the pause command.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IReadOnlyList<ServerOperation>> PauseServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.PauseServer, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<IReadOnlyList<ServerOperation>> PowerOnServer(List<string> serverIds)
        {
            return await PowerOnServer(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the power on operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to power on a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the power on command. 
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IReadOnlyList<ServerOperation>> PowerOnServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.PowerOnServer, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<IReadOnlyList<ServerOperation>> PowerOffServer(List<string> serverIds)
        {
            return await PowerOffServer(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the power off operation to a list of servers and adds operation to queue. 
        /// Use this operation when you want to power off a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the power off command.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IReadOnlyList<ServerOperation>> PowerOffServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.PowerOffServer, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<IReadOnlyList<ServerOperation>> RebootServer(List<string> serverIds)
        {
            return await RebootServer(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the reboot operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to reboot a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the reboot command. 
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IReadOnlyList<ServerOperation>> RebootServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.RebootServer, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<IReadOnlyList<ServerOperation>> ShutDownServer(List<string> serverIds)
        {
            return await ShutDownServer(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the shut down operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to shut down a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the shut down command.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IReadOnlyList<ServerOperation>> ShutDownServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.ShutDownServer, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
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
        public async Task<IReadOnlyList<ServerOperation>> ResetServer(List<string> serverIds)
        {
            return await ResetServer(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the reset operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to reset a single server or group of servers. 
        /// It should be used in conjunction with the  Queue GetStatus operation to check the result of the reset command. 
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IReadOnlyList<ServerOperation>> ResetServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var requestModel = new ServiceRequestModel() { UnNamedArray = serverIds.ToArray() };

            var serviceRequest = new ServiceRequest()
            {
                ServiceUri = string.Format(Constants.ServiceUris.Server.ResetServer, userAuthentication.AccountAlias),
                BearerToken = userAuthentication.BearerToken,
                RequestModel = requestModel,
                HttpMethod = HttpMethod.Post
            };

            var result = await ServiceInvoker.Invoke<ServiceRequest, ServerPowerOpsResponse>(serviceRequest, cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                var response = result.Response;
                return response;
            }

            return null;
        }
    }
}
