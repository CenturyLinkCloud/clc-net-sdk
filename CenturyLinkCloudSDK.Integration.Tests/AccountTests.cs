using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CenturyLinkCloudSDK.ServiceModels;
using System.Threading.Tasks;
using System.Linq;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class AccountTests
    {
        const string TestServer = "ca1p2o2server01";
        const string TestGroup = "a726bd9f7d9be411877f005056882d41";
        private static Client client;
        private static Authentication authentication;

        [ClassInitialize]
        public static void Login(TestContext testContext)
        {
            client = new Client(Credentials.Username, Credentials.Password);
            authentication = client.Authentication;
        }

        [TestMethod]
        public async Task GetRecentActivityReturnsRecentActivity()
        {
            var activity = await client.Account.GetRecentActivity().ConfigureAwait(false);
            Assert.IsTrue(activity.ToList().Count > 0);
        }


        [TestMethod]
        public async Task GetRecentActivityForServerReturnsRecentActivity()
        {
            var activity = await client.Account.GetRecentActivityFor(new Server { Id = TestServer }).ConfigureAwait(false);
            Assert.IsTrue(activity.ToList().Count > 0);
            Assert.IsTrue(activity.All(a => TestServer.Equals(a.ReferenceId, StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public async Task GetRecentActivityForGroupReturnsRecentActivity()
        {
            var group = await client.Groups.GetGroup(TestGroup);

            var activity = await client.Account.GetRecentActivityFor(group).ConfigureAwait(false);
            //this is blunt, but it's the best we can do
            Assert.IsTrue(activity.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetRecentActivityForDataCenterReturnsRecentActivity()
        {
            var dc = await client.DataCenters.GetDataCenter("ca1", includeTotalAssets: false);
            var activity = await client.Account.GetRecentActivityFor(dc).ConfigureAwait(false);
            
            //this is blunt, but it's the best we can do
            Assert.IsTrue(activity.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetRecentActivityForEmptyDataCenterReturnsEmptyActivity()
        {
            var dc = await client.DataCenters.GetDataCenter("ca2", includeTotalAssets: false);
            var activity = await client.Account.GetRecentActivityFor(dc).ConfigureAwait(false);
            
            Assert.IsTrue(activity.ToList().Count == 0);
        }
    }
}
