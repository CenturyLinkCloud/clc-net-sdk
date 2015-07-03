using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.ServiceModels.Requests.Group;
using CenturyLinkCloudSDK.ServiceModels.Requests.Server;
using CenturyLinkCloudSDK.ServiceModels.Responses.Servers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
        internal ServerService(Authentication authentication, IServiceInvoker serviceInvoker)
            : base(authentication, serviceInvoker)
        {

        }

        void SetInternalServerProperties(Server server)
        {
            server.ServerService = this;
        }

        /// <summary>
        /// Gets the details for a individual server.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources.
        /// </summary>        
        /// <param name="serverId">The id of the server</param>
        /// <returns>The server</returns>
        public Task<Server> GetServer(string serverId)
        {
            return GetServer(serverId, CancellationToken.None);
        }

        /// <summary>
        /// Gets the details for a individual server.
        /// Use this operation when you want to find out all the details for a server. 
        /// It can be used to look for changes, its status, or to retrieve links to associated resources.
        /// </summary>
        /// <param name="serverId">The id of the server</param>        
        /// <returns>The server</returns>
        public Task<Server> GetServer(string serverId, CancellationToken cancellationToken)
        {
            var uri = string.Format(Constants.ServiceUris.Server.GetServer, Configuration.BaseUri, authentication.AccountAlias, serverId);
            return GetServerByLink(uri, cancellationToken);
        }

        /// <summary>
        /// Gets the details for a set of servers in parallel where possible
        /// </summary>
        /// <param name="serverIds">The list of servers to load</param>
        /// <returns></returns>
        public Task<IEnumerable<Server>> GetServers(IEnumerable<string> serverIds)
        {
            return GetServers(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Gets the details for a set of servers in parallel where possible
        /// </summary>
        /// <param name="serverIds">The list of servers to load</param>
        /// <returns></returns>
        public async Task<IEnumerable<Server>> GetServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            //TODO: fetch concurrently
            var servers = new List<Server>();
            foreach(var s in serverIds)
            {
                servers.Add(await GetServer(s, cancellationToken));
            }

            return servers;
        }

        internal async Task<Server> GetServerByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<Server>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            SetInternalServerProperties(result);
            
            return result;
        }

        internal async Task<ServerCredential> GetServerCredentialsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return await serviceInvoker.Invoke<ServerCredential>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<Statistics> GetServerStatisticsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return await serviceInvoker.Invoke<Statistics>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }
        
        internal async Task<IEnumerable<ServerOperation>> PauseServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PauseServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> PowerOnServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PowerOnServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> PowerOffServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PowerOffServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> RebootServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.RebootServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> ShutDownServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.ShutDownServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> ResetServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.ResetServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> StartMaintenance(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.StartMaintenance, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<IEnumerable<ServerOperation>> StopMaintenance(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.StopMaintenance, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return await serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }


        internal async Task<Link> SetDisks(string serverId, IEnumerable<DiskPatchOperation> operations, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(new HttpMethod("PATCH"), string.Format(Constants.ServiceUris.Server.UpdateResources, Configuration.BaseUri, authentication.AccountAlias, serverId), operations);
            return await serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<Link> SetCpuAndMemory(string serverId, IEnumerable<CpuMemoryPatchOperation> operations, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(new HttpMethod("PATCH"), string.Format(Constants.ServiceUris.Server.UpdateResources, Configuration.BaseUri, authentication.AccountAlias, serverId), operations);
            return await serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }
        
        /*                
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
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.CreateServer, Configuration.BaseUri, authentication.AccountAlias), request);
            var result = await serviceInvoker.Invoke<CreateServerResponse>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
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
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, string.Format(Constants.ServiceUris.Server.RUDPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId, publicIp));
            var result = await serviceInvoker.Invoke<PublicIpAddress>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

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
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.AddPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId), publicIpRequestContent);
            var result = await serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

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
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Delete, string.Format(Constants.ServiceUris.Server.RUDPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId, publicIp));
            var result = await serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

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
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Put, string.Format(Constants.ServiceUris.Server.RUDPublicIpAddress, Configuration.BaseUri, authentication.AccountAlias, serverId, publicIp), publicIpRequestContent);
            var result = await serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            return result;
        }                
*/
    }
}
