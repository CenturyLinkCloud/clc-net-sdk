using CenturyLinkCloudSDK.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class GroupsTests
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
        public async Task GetGroupReturnValidData()
        {
            var result = await client.Groups.GetGroup("00e3ce61918fe411877f005056882d41").ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Id));
            Assert.IsTrue(result.Id == "00e3ce61918fe411877f005056882d41");
        }

        [TestMethod]
        public async Task GetGroupServersReturnValidData()
        {
            var result = await client.Groups.GetGroup("a726bd9f7d9be411877f005056882d41").ConfigureAwait(false);
            var servers = await result.GetServers(includeSubGroups: false).ConfigureAwait(false);
            Assert.IsTrue(servers.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetGroupDefaultSettingsReturnValidData()
        {
            var group = await client.Groups.GetGroup("a726bd9f7d9be411877f005056882d41").ConfigureAwait(false);
            var groupSettings = await group.GetDefaultSettings().ConfigureAwait(false);

            Assert.IsTrue(groupSettings.MemoryGB.Value > 0);
        }

        /*
       
        [TestMethod]
        public async Task GetGroupHierarchyByIdReturnValidData()
        {
            var groupHierarchy = await client.Groups.GetGroupHierarchy("00e3ce61918fe411877f005056882d41", true).ConfigureAwait(false);

            Assert.IsTrue(groupHierarchy.Groups.Count > 0);
        }

        [TestMethod]
        public async Task GetGroupHierarchyReturnValidData()
        {
            //var group = await client.Groups.GetGroup("a726bd9f7d9be411877f005056882d41").ConfigureAwait(false);
            var group = await client.Groups.GetGroup("00e3ce61918fe411877f005056882d41").ConfigureAwait(false);          
            var groupHierarchy = await client.Groups.GetGroupHierarchy(group, true).ConfigureAwait(false);

            Assert.IsTrue(groupHierarchy.Groups.Count > 0);
        }

         */
    }
}
