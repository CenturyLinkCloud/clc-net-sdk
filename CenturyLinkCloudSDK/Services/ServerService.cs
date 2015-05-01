using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.Group;
using CenturyLinkCloudSDK.ServiceModels.Requests.Server;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Services
{
    /// <summary>
    /// This class contains operations associated with servers.
    /// </summary>
    public class ServerService : ServiceBase
    {
        internal ServerService(Authentication authentication)
            : base(authentication)
        {

        }

        /// <summary>
        /// Gets the details for a individual server.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverId"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        public async Task<Server> GetServer(string serverId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Server.GetServer, Configuration.BaseUri, authentication.AccountAlias, serverId);
            return await GetServerByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the pause operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to pause a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the pause command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> PauseServer(List<string> serverIds)
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
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> PauseServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PauseServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sends the power on operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to power on a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the power on command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> PowerOnServer(List<string> serverIds)
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
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> PowerOnServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PowerOnServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sends the power off operation to a list of servers and adds operation to queue. 
        /// Use this operation when you want to power off a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the power off command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> PowerOffServer(List<string> serverIds)
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
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> PowerOffServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PowerOffServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sends the reboot operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to reboot a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the reboot command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> RebootServer(List<string> serverIds)
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
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> RebootServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.RebootServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sends the shut down operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to shut down a single server or group of servers. 
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the shut down command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> ShutDownServer(List<string> serverIds)
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
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> ShutDownServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.ShutDownServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sends the reset operation to a list of servers and adds operation to queue.
        /// Use this operation when you want to reset a single server or group of servers. 
        /// It should be used in conjunction with the  Queue GetStatus operation to check the result of the reset command.
        /// </summary>
        /// <param name="accountAlias"></param>
        /// <param name="serverIds"></param>
        /// <returns>An asynchronous Task of IEnumerable of ServerOperation.</returns>
        public async Task<IEnumerable<ServerOperation>> ResetServer(List<string> serverIds)
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
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> ResetServer(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.ResetServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Starts server maintenance.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> StartMaintenance(List<string> serverIds)
        {
            return await StartMaintenance(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Starts server maintenance.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> StartMaintenance(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.StartMaintenance, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Stops server maintenance.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> StopMaintenance(List<string> serverIds)
        {
            return await StopMaintenance(serverIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops server maintenance.
        /// </summary>
        /// <param name="serverIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerOperation>> StopMaintenance(List<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.StopMaintenance, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            var result = await ServiceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the server statistics.
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public async Task<Statistics> GetServerStatistics(string serverId)
        {
            return await GetServerStatistics(serverId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the server statistics.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Statistics> GetServerStatistics(string serverId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Server.GetServerStatistics, Configuration.BaseUri, authentication.AccountAlias, serverId, Constants.ServiceUris.Querystring.GetLatestStatistics);
            return await GetServerStatisticsByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a server.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateServerResponse> CreateServer(CreateServerRequest request)
        {
            return await CreateServer(request, CancellationToken.None).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Creates a server.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateServerResponse> CreateServer(CreateServerRequest request, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.CreateServer, Configuration.BaseUri, authentication.AccountAlias), request);
            var result = await ServiceInvoker.Invoke<CreateServerResponse>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the server credentials.
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public async Task<ServerCredential> GetServerCredentials(string serverId)
        {
            return await GetServerCredentials(serverId, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the server credentials.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ServerCredential> GetServerCredentials(string serverId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Server.GetServerCredentials, Configuration.BaseUri, authentication.AccountAlias, serverId);
            return await GetServerCredentialsByLink(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIp"></param>
        /// <returns></returns>
        public async Task<PublicIpAddress> GetPublicIpAddress(string serverId, string publicIp)
        {
            return await GetPublicIpAddress(serverId, publicIp, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PublicIpAddress> GetPublicIpAddress(string serverId, string publicIp, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Server.RUDPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId, publicIp));
            var result = await ServiceInvoker.Invoke<PublicIpAddress>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Adds a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIpRequestContent"></param>
        /// <returns></returns>
        public async Task<Link> AddPublicIpAddress(string serverId, PublicIpAddress publicIpRequestContent)
        {
            return await AddPublicIpAddress(serverId, publicIpRequestContent, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIpRequestContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Link> AddPublicIpAddress(string serverId, PublicIpAddress publicIpRequestContent, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.AddPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId), publicIpRequestContent);
            var result = await ServiceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Removes a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIp"></param>
        /// <returns></returns>
        public async Task<Link> RemovePublicIpAddress(string serverId, string publicIp)
        {
            return await RemovePublicIpAddress(serverId, publicIp, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Removes a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Link> RemovePublicIpAddress(string serverId, string publicIp, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Delete, string.Format(Constants.ServiceUris.Server.RUDPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId, publicIp));
            var result = await ServiceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Updates a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIp"></param>
        /// <param name="publicIpRequestContent"></param>
        /// <returns></returns>
        public async Task<Link> UpdatePublicIpAddress(string serverId, string publicIp, PublicIpAddress publicIpRequestContent)
        {
            return await UpdatePublicIpAddress(serverId, publicIp, publicIpRequestContent, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates a public ip address.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="publicIp"></param>
        /// <param name="publicIpRequestContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Link> UpdatePublicIpAddress(string serverId, string publicIp, PublicIpAddress publicIpRequestContent, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Put, string.Format(Constants.ServiceUris.Server.RUDPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId, publicIp), publicIpRequestContent);
            var result = await ServiceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sets cpu and memory
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public async Task<Link> SetCpuAndMemory(string serverId, IEnumerable<CpuMemoryPatchOperation> operations)
        {
            return await SetCpuAndMemory(serverId, operations, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets cpu and memory
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="operations"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Link> SetCpuAndMemory(string serverId, IEnumerable<CpuMemoryPatchOperation> operations, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(new HttpMethod("PATCH"), string.Format(Constants.ServiceUris.Server.UpdateResources, Configuration.BaseUri, authentication.AccountAlias, serverId), operations);
            var result = await ServiceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Sets disk properties
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public async Task<Link> SetDisks(string serverId, IEnumerable<DiskPatchOperation> operations)
        {
            return await SetDisks(serverId, operations, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets disk properties
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="operations"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Link> SetDisks(string serverId, IEnumerable<DiskPatchOperation> operations, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(new HttpMethod("PATCH"), string.Format(Constants.ServiceUris.Server.UpdateResources, Configuration.BaseUri, authentication.AccountAlias, serverId), operations);
            var result = await ServiceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds)
        {

            return await GetRecentActivity(referenceIds, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, CancellationToken cancellationToken)
        {

            return await GetRecentActivity(referenceIds, 10, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordCountLimit)
        {

            return await GetRecentActivity(referenceIds, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent data center activity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> accounts, IEnumerable<string> referenceIds, int recordCountLimit)
        {
            return await GetRecentActivity(accounts, referenceIds, recordCountLimit, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <param name="referenceIds"></param>
        /// <param name="recordCountLimit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> referenceIds, int recordCountLimit, CancellationToken cancellationToken)
        {
            var accounts = new List<string>();
            accounts.Add(authentication.AccountAlias);

            return await GetRecentActivity(accounts, referenceIds, recordCountLimit, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns recent group activity.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetRecentActivity(IEnumerable<string> accounts, IEnumerable<string> referenceIds, int recordCountLimit, CancellationToken cancellationToken)
        {
            var requestModel = new GetRecentActivityRequest()
            {
                EntityTypes = new List<string>() { Constants.EntityTypes.Server },
                ReferenceIds = referenceIds,
                Accounts = accounts,
                Limit = recordCountLimit
            };

            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Group.GetRecentActivity, Configuration.BaseUri), requestModel);
            var result = await ServiceInvoker.Invoke<IEnumerable<Activity>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the server statistics.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<Statistics> GetServerStatisticsByLink(string uri)
        {
            return await GetServerStatisticsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the server statistics. 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<Statistics> GetServerStatisticsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<Statistics>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the details for a individual server by hypermedia link.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources. 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<Server> GetServerByLink(string uri)
        {
            return await GetServerByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the details for a individual server by hypermedia link.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources. 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<Server> GetServerByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<Server>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            result.Authentication = authentication;

            return result;
        }

        /// <summary>
        /// Gets the server credentials
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<ServerCredential> GetServerCredentialsByLink(string uri)
        {
            return await GetServerCredentialsByLink(uri, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the server credentials
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<ServerCredential> GetServerCredentialsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await ServiceInvoker.Invoke<ServerCredential>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
