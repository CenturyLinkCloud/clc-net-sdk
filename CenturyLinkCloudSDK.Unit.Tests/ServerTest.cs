using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using System.Net.Http;
using System.Net.Http.Headers;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Requests;
using CenturyLinkCloudSDK.ServiceModels.V2.Authentication.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class ServerTest
    {
        [TestInitialize]
        public void Login()
        {
            var authentication = new Authentication();
            var result = authentication.Login("mario.mamalis", "MarioTest!").Result;
        }

        [TestMethod]
        public async Task GetServerReturnValidData()
        {
            var serverContext = new Server();
            var result = await serverContext.GetServer(Persistence.UserInfo.AccountAlias, "CA1P2O2DF2TST01");

            Assert.IsNotNull(result);
            Assert.IsTrue(!String.IsNullOrEmpty(result.Id));
        }

        [TestMethod]
        public async Task PauseServersReturnPauseOperationIsQueuedIfValidState()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Server();
            var serverOperationResponse = await serverContext.PauseServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse.Response)
                {
                    if (string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);
                    }
                }
            }
        }

        [TestMethod]
        public async Task PowerOnServersReturnPowerOnOperationIsQueuedIfValidState()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Server();
            var serverOperationResponse = await serverContext.PowerOnServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse.Response)
                {
                    if(string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);
                    }
                }
            }
        }

        [TestMethod]
        public async Task PowerOffServersReturnPowerOffOperationIsQueuedIfValidState()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Server();
            var serverOperationResponse = await serverContext.PowerOffServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse.Response)
                {
                    if (string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);
                    }
                }
            }
        }


        [TestMethod]
        public async Task RebootServersReturnRebootOperationIsQueuedIfValidState()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Server();
            var serverOperationResponse = await serverContext.RebootServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse.Response)
                {
                    if (string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);
                    }
                }
            }
        }

        [TestMethod]
        public async Task ShutDownServersReturnShutDownOperationIsQueuedIfValidState()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Server();
            var serverOperationResponse = await serverContext.ShutDownServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse.Response)
                {
                    if (string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);
                    }
                }
            }
        }

        [TestMethod]
        public async Task ResetServersReturnResetOperationIsQueuedIfValidState()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Server();
            var serverOperationResponse = await serverContext.ResetServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse.Response)
                {
                    if (string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);
                    }
                }
            }
        }
    }
}
