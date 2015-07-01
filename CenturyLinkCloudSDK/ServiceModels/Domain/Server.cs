using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class Server
    {
        Lazy<Link> statisticsLink;
        Lazy<Link> credentialsLink;

        [JsonPropertyAttribute]
        private IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Server()
        {
            statisticsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "statistics", StringComparison.CurrentCultureIgnoreCase));
            });

            credentialsLink = new Lazy<Link>(() =>
            {
                return Links.FirstOrDefault(l => String.Equals(l.Rel, "credentials", StringComparison.CurrentCultureIgnoreCase));
            });
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public bool IsTemplate { get; set; }
        public string LocationId { get; set; }
        public string OsType { get; set; }
        public string Status { get; set; }
        public ServerDetail Details { get; set; }
        public string Type { get; set; }
        public string StorageType { get; set; }
        public ChangeInfo ChangeInfo { get; set; }

        internal ServerService ServerService { get; set; }

        /// <summary>
        /// Determines if this server has statistics
        /// </summary>
        public bool HasStatistics
        {
            get { return statisticsLink.Value != null; }
        }

        /// <summary>
        /// Determines if the credentials link is available.
        /// </summary>
        /// <returns></returns>
        public bool HasCredentials
        {
            get { return credentialsLink.Value != null; }
        }

        /// <summary>
        /// Gets the server credentials.
        /// </summary>
        /// <returns>The credentials</returns>
        public Task<ServerCredential> GetCredentials()
        {
            return GetCredentials(CancellationToken.None);
        }

        /// <summary>
        /// Gets the server credentials.
        /// </summary>        
        /// <returns>The credentials</returns>
        public async Task<ServerCredential> GetCredentials(CancellationToken cancellationToken)
        {
            if (!HasCredentials)
            {
                return null;
            }

            return await ServerService.GetServerCredentialsByLink(string.Format("{0}{1}", Configuration.BaseUri, credentialsLink.Value.Href), cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the statistics for this server
        /// </summary>
        /// <returns>The statistics</returns>
        public Task<Statistics> GetStatistics()
        {
            return GetStatistics(CancellationToken.None);
        }

        /// <summary>
        /// Gets the statistics for this server
        /// </summary>
        /// <returns>The statistics</returns>
        public async Task<Statistics> GetStatistics(CancellationToken cancellationToken)
        {
            if(!HasStatistics)
            {
                return null;
            }

            return await ServerService.GetServerStatisticsByLink(string.Format("{0}{1}{2}", Configuration.BaseUri, statisticsLink.Value.Href, Constants.ServiceUris.Querystring.GetLatestStatistics), cancellationToken).ConfigureAwait(false);
        }

        #region Power operations
        /// <summary>
        /// Adds a job to the queue to pause this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> Pause()
        {
            return Pause(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to pause this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> Pause(CancellationToken cancellationToken)
        {
            var result = await ServerService.PauseServers(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to power on this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> PowerOn()
        {
            return PowerOn(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to power on this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> PowerOn(CancellationToken cancellationToken)
        {
            var result = await ServerService.PowerOnServers(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to power off this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> PowerOff()
        {
            return PowerOff(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to power off this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> PowerOff(CancellationToken cancellationToken)
        {
            var result = await ServerService.PowerOffServers(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to reboot this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> Reboot()
        {
            return Reboot(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to reboot this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> Reboot(CancellationToken cancellationToken)
        {
            var result = await ServerService.RebootServers(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to reset this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> Reset()
        {
            return Reset(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to reset this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> Reset(CancellationToken cancellationToken)
        {
            var result = await ServerService.ResetServers(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to shutdown this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> ShutDown()
        {
            return ShutDown(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to shutdown this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> ShutDown(CancellationToken cancellationToken)
        {
            var result = await ServerService.ShutDownServers(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to start maintenance on this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> StartMaintenance()
        {
            return StartMaintenance(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to start maintenance on this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> StartMaintenance(CancellationToken cancellationToken)
        {
            var result = await ServerService.StartMaintenance(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }

        /// <summary>
        /// Adds a job to the queue to stop maintenance on this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>                
        public Task<ServerOperation> StopMaintenance()
        {
            return StopMaintenance(CancellationToken.None);
        }

        /// <summary>
        /// Adds a job to the queue to stop maintenance on this server.        
        /// It should be used in conjunction with the Queue GetStatus operation to check the result of the command.
        /// </summary>        
        public async Task<ServerOperation> StopMaintenance(CancellationToken cancellationToken)
        {
            var result = await ServerService.StopMaintenance(new[] { Id }, cancellationToken).ConfigureAwait(false);
            return result.Single();
        }
        #endregion

        #region Resource operations
        public Task<Link> SetDisks(IEnumerable<DiskPatchOperation> operations)
        {
            return SetDisks(operations, CancellationToken.None);
        }

        public Task<Link> SetDisks(IEnumerable<DiskPatchOperation> operations, CancellationToken cancellationToken)
        {
            return ServerService.SetDisks(Id, operations, cancellationToken);
        }

        public Task<Link> SetCpuAndMemory(IEnumerable<CpuMemoryPatchOperation> operations)
        {
            return SetCpuAndMemory(operations, CancellationToken.None);
        }

        public Task<Link> SetCpuAndMemory(IEnumerable<CpuMemoryPatchOperation> operations, CancellationToken cancellationToken)
        {
            return ServerService.SetCpuAndMemory(Id, operations, cancellationToken);
        }
        #endregion

    }
}
