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
        public async Task PauseServersReturnPauseOperationIsQueuedIfRunning()
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
        public async Task PowerOnServersReturnPowerOnOperationIsQueuedIfRunning()
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

    }
}
