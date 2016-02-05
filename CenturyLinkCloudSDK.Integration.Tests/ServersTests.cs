using CenturyLinkCloudSDK.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CenturyLinkCloudSDK.ServiceModels.Requests.Server;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class ServersTests
    {        
        private static Client client;
        private static Authentication userAuthentication;

        [ClassInitialize]
        public static void Login(TestContext testContext)
        {
            client = new Client(Credentials.Username, Credentials.Password);
            userAuthentication = client.Authentication;
        }

        [TestMethod]
        public async Task GetServerReturnValidData()
        {
            var result = await client.Servers.GetServer("ca1p2o2server01");

            Assert.IsNotNull(result);
            Assert.IsTrue(!String.IsNullOrEmpty(result.Id));
        }

        [TestMethod]
        public async Task GetServersReturnValidData()
        {
            var result = await client.Servers.GetServers(new [] { "ca1p2o2server01", "CA1P2O2DF2TST01" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public async Task GetServerCredentialsReturnValidData()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var result = await server.GetCredentials();

            Assert.IsNotNull(result);
            Assert.IsTrue(!String.IsNullOrEmpty(result.UserName));
            Assert.IsTrue(!String.IsNullOrEmpty(result.Password));
        }

        [TestMethod]
        public async Task GetServerStatisticsReturnValidData()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var result = await server.GetStatistics();

            Assert.IsNotNull(result);
        }

        [Ignore]
        [TestMethod]
        public async Task CanCreateServer()
        {
            var dc = await client.DataCenters.GetDataCenter("ca1", includeTotalAssets: false);
            var capabilities = await dc.GetDeploymentCapabilities();
            
            var result =
                await client.Servers.CreateServer(
                    new CreateStandardServerRequest("mstest", "a726bd9f7d9be411877f005056882d41", capabilities.Templates.First().Name, 1, 1)
                    {
                        Description = "A server created by the automated test suite"
                    });

            Assert.IsTrue(result.IsQueued);
        }
        
        [Ignore]
        [TestMethod]
        public async Task PauseServersReturnOperationIsQueuedIfValidState()
        {            
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var response = await server.Pause();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task PowerOnServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var response = await server.PowerOn();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task PowerOffServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var response = await server.PowerOff();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task RebootServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var response = await server.Reboot();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task ResetServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var response = await server.Reset();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task ShutDownServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01");
            var response = await server.ShutDown();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task StartMaintenanceServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("ca1p2o2df2tst01");
            var response = await server.StartMaintenance();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [Ignore]
        [TestMethod]
        public async Task StopMaintenaceServersReturnOperationIsQueuedIfValidState()
        {
            var server = await client.Servers.GetServer("ca1p2o2df2tst01");
            var response = await server.StopMaintenance();

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                Assert.IsTrue(response.IsQueued);
            }
        }

        [TestMethod]
        public async Task SetCpuAndMemory()
        {
            var operations = new List<CpuMemoryPatchOperation>();

            var patchCpuOperation = new CpuMemoryPatchOperation()
            {
                Op = "set",
                Member = "cpu",
                Value = 1
            };

            var patchMemoryOperation = new CpuMemoryPatchOperation()
            {
                Op = "set",
                Member = "memory",
                Value = 2
            };

            operations.Add(patchCpuOperation);
            operations.Add(patchMemoryOperation);

            var server = await client.Servers.GetServer("ca1p2o2df2tst01");
            var result = await server.SetCpuAndMemory(operations);

            Assert.IsNotNull(result);
        }
    }
}
