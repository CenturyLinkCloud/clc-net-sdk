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

        void TryDeleteGroup(string groupId)
        {
            try
            {
                client.Groups.DeleteGroup(groupId).Wait();
            }catch(Exception ex)
            {
                throw new Exception("Failed to delete group", ex);
            }
        }
        
        [TestMethod]
        public async Task CanCreateGroup()
        {
            Group newGroup = default(Group);
            try
            {
                var name = "Automated test group";
                var description = "A group created via automated testing";
                var parentId = "00e3ce61918fe411877f005056882d41";

                newGroup =
                    await client.Groups.CreateGroup(
                        new ServiceModels.Requests.Group.CreateGroupRequest(name, parentId)
                        {
                            Description = description,
                        });

                Assert.AreEqual(newGroup.Name, name);
                Assert.AreEqual(newGroup.Description, description);

                var parent = await client.Groups.GetGroup(parentId);
                Assert.IsTrue(parent.Groups.Any(g => g.Id == newGroup.Id));
            } finally
            {
                if (newGroup != null) TryDeleteGroup(newGroup.Id);
            }     
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
        public async Task GetGroupServersIncludingSubGroupsReturnsValidData()
        {
            var result = await client.Groups.GetGroup("a726bd9f7d9be411877f005056882d41").ConfigureAwait(false);            
            var servers = await result.GetServers(includeSubGroups: true).ConfigureAwait(false);
            Assert.IsTrue(servers.ToList().Count > 0);            
        }

        [TestMethod]
        public async Task GetGroupDefaultSettingsReturnValidData()
        {
            var group = await client.Groups.GetGroup("a726bd9f7d9be411877f005056882d41").ConfigureAwait(false);
            var groupSettings = await group.GetDefaultSettings().ConfigureAwait(false);

            Assert.IsTrue(groupSettings.MemoryGB.Value > 0);
        }        
    }
}
