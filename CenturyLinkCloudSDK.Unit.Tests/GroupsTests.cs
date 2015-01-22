using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class GroupsTests
    {
        [TestInitialize]
        public void Login()
        {
            var authentication = new Authentication();
            var result = authentication.Login("mario.mamalis", "MarioTest!").Result;
        }

        [TestMethod]
        public async Task GetGroupReturnValidData()
        {
            var groupContext = new Groups();
            var result = await groupContext.GetGroup(Persistence.UserInfo.AccountAlias, "ca1-42311");

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Id));
            Assert.IsTrue(result.Id == "ca1-42311");
        }

        [TestMethod]
        public async Task GetGroupByHyperlinkReturnValidData()
        {
            var groupContext = new Groups();
            var result = await groupContext.GetGroup("/v2/groups/p2o2/ca1-42311");

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Id));
            Assert.IsTrue(result.Id == "ca1-42311");
        }
    }
}
