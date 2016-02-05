﻿using CenturyLinkCloudSDK.Runtime;
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

using CenturyLinkCloudSDK.Extensions;

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
        public Task<IEnumerable<Server>> GetServers(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {            
            return serverIds.SelectEachAsync(GetServer, cancellationToken);
        }

        #region Power operations
        /// <summary>
        /// Adds a job to the queue to pause the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> Pause(IEnumerable<string> serverIds)
        {
            return Pause(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to pause the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> Pause(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PauseServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Adds a job to the queue to power on the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> PowerOn(IEnumerable<string> serverIds)
        {
            return PowerOn(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to power on the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>  
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> PowerOn(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PowerOnServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Adds a job to the queue to power off the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> PowerOff(IEnumerable<string> serverIds)
        {
            return PowerOff(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to power off the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public Task<IEnumerable<ServerOperation>> PowerOff(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.PowerOffServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);          
        }

        /// <summary>
        /// Adds a job to the queue to reboot the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> Reboot(IEnumerable<string> serverIds)
        {
            return Reboot(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to reboot the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public Task<IEnumerable<ServerOperation>> Reboot(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.RebootServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);          
        }

        /// <summary>
        /// Adds a job to the queue to reset the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary> 
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> Reset(IEnumerable<string> serverIds)
        {
            return Reset(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to reset the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>    
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> Reset(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.ResetServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Adds a job to the queue to shutdown the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>    
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> ShutDown(IEnumerable<string> serverIds)
        {
            return ShutDown(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to shutdown the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public Task<IEnumerable<ServerOperation>> ShutDown(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.ShutDownServer, Configuration.BaseUri, authentication.AccountAlias), serverIds.ToArray());
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);
        }

        /// <summary>
        /// Adds a job to the queue to start maintenance on the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<IEnumerable<ServerOperation>> StartMaintenance(IEnumerable<string> serverIds)
        {
            return StartMaintenance(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to start maintenance on the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public Task<IEnumerable<ServerOperation>> StartMaintenance(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            //The API currently has an issue that requires the server ids to be in all caps or else this won't work
            var ids = serverIds.Select(i => i.ToUpper()).ToArray();
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.StartMaintenance, Configuration.BaseUri, authentication.AccountAlias), ids);
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);           
        }

        /// <summary>
        /// Adds a job to the queue to stop maintenance on the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>             
        /// <param name="serverIds">The server ids</param>
        public Task<IEnumerable<ServerOperation>> StopMaintenance(IEnumerable<string> serverIds)
        {
            return StopMaintenance(serverIds, CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to stop maintenance on the servers.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public Task<IEnumerable<ServerOperation>> StopMaintenance(IEnumerable<string> serverIds, CancellationToken cancellationToken)
        {
            //The API currently has an issue that requires the server ids to be in all caps or else this won't work
            var ids = serverIds.Select(i => i.ToUpper()).ToArray();
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.StopMaintenance, Configuration.BaseUri, authentication.AccountAlias), ids);
            return serviceInvoker.Invoke<IEnumerable<ServerOperation>>(httpRequestMessage, cancellationToken);
        }
        #endregion

        internal async Task<Server> GetServerByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            var result = await serviceInvoker.Invoke<Server>(httpRequestMessage, cancellationToken).ConfigureAwait(false);

            SetInternalServerProperties(result);
            
            return result;
        }

        internal Task<ServerCredential> GetServerCredentialsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<ServerCredential>(httpRequestMessage, cancellationToken);
        }

        internal Task<Statistics> GetServerStatisticsByLink(string uri, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Get, uri);
            return serviceInvoker.Invoke<Statistics>(httpRequestMessage, cancellationToken);
        }
                 
        internal Task<Link> SetDisks(string serverId, IEnumerable<DiskPatchOperation> operations, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(new HttpMethod("PATCH"), string.Format(Constants.ServiceUris.Server.UpdateResources, Configuration.BaseUri, authentication.AccountAlias, serverId), operations);
            return serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken);
        }

        internal Task<Link> SetCpuAndMemory(string serverId, IEnumerable<CpuMemoryPatchOperation> operations, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(new HttpMethod("PATCH"), string.Format(Constants.ServiceUris.Server.UpdateResources, Configuration.BaseUri, authentication.AccountAlias, serverId), operations);
            return serviceInvoker.Invoke<Link>(httpRequestMessage, cancellationToken);
        }
                           
        /// <summary>
        /// Creates a server.
        /// </summary>
        /// <param name="request">The details of the server to create</param>
        /// <returns>The response details</returns>
        public Task<CreateServerResponse> CreateServer(CreateServerRequest request)
        {
            return CreateServer(request, CancellationToken.None);
        }

        /// <summary>
        /// Creates a server.
        /// </summary>
        /// <param name="request">The details of the server to create</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The response details</returns>
        public Task<CreateServerResponse> CreateServer(CreateServerRequest request, CancellationToken cancellationToken)
        {
            var httpRequestMessage = CreateAuthorizedHttpRequestMessage(HttpMethod.Post, string.Format(Constants.ServiceUris.Server.CreateServer, Configuration.BaseUri, authentication.AccountAlias), request);
            return serviceInvoker.Invoke<CreateServerResponse>(httpRequestMessage, cancellationToken);            
        }        
    }
}
