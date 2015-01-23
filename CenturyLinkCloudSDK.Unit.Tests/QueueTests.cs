using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using System.Collections.Generic;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using System.Linq;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class QueueTests
    {
        [TestInitialize]
        public void Login()
        {
            var authentication = new Authentication();
            var result = authentication.Login("mario.mamalis", "MarioTest!").Result;
        }

        [TestMethod]
        public async Task GetQueueStatusReturnValidData()
        {
            var serverIds = new List<string>() { "CA1P2O2DF2TST01", "CA1P2O2TEST01" };

            var serverContext = new Servers();
            var serverOperationResponse = await serverContext.ResetServer(Persistence.UserInfo.AccountAlias, serverIds);

            if (serverOperationResponse != null)
            {
                foreach (var server in serverOperationResponse)
                {
                    if (string.IsNullOrEmpty(server.ErrorMessage))
                    {
                        Assert.IsTrue(server.IsQueued);

                        var status = server.Links.Where(l => l.Rel == "status").FirstOrDefault();

                        if (status != null)
                        {
                            var statusId = status.Id;
                            var queueContext = new Queues();
                            var queue = await queueContext.GetStatus(Persistence.UserInfo.AccountAlias, statusId);

                            Assert.IsTrue(!String.IsNullOrEmpty(queue.Status));
                        }
                    }
                }
            }
        }
    }
}
