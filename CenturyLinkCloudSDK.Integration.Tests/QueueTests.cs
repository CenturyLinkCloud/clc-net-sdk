using CenturyLinkCloudSDK.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class QueueTests
    {        
        private static Client client;
        private static Authentication userAuthentication;

        [ClassInitialize]
        public static void Login(TestContext testContext)
        {
            client = new Client(Credentials.Username, Credentials.Password);
            userAuthentication = client.Authentication;
        }

        //[TestMethod]
        //public async Task GetQueueStatusReturnValidData()
        //{
        //    var serverIds = new List<string>() { "CA1P2O2DF2TST01" };

        //    var serverOperationResponse = await client.Servers.PowerOnServer(serverIds);

        //    if (serverOperationResponse != null)
        //    {
        //        foreach (var server in serverOperationResponse)
        //        {
        //            if (string.IsNullOrEmpty(server.ErrorMessage))
        //            {
        //                Assert.IsTrue(server.IsQueued);

        //                //var status = server.Links.Where(l => l.Rel == "status").FirstOrDefault();

        //                //if (status != null)
        //                //{

        //                var statusId = server.QueueStatusId;
        //                var queue = await client.Queues.GetStatus(statusId);

        //                Assert.IsTrue(!String.IsNullOrEmpty(queue.Status));

        //               // }
        //            }
        //        }
        //    }
        //}         
    }
}
