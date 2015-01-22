using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class DataCenterTest
    {
        [TestInitialize]
        public void Login()
        {
            var authentication = new Authentication();
            var result = authentication.Login("mario.mamalis", "MarioTest!").Result;
        }

        [TestMethod]
        public async Task GetDataCentersReturnValidData()
        {
            var dataCenterContext = new DataCenters();
            var result = await dataCenterContext.GetDataCenters(Persistence.UserInfo.AccountAlias);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetDataCenterGroupsReturnValidData()
        {
            var dataCenterContext = new DataCenters();
            var result = await dataCenterContext.GetDataCenterGroups(Persistence.UserInfo.AccountAlias, "CA2", true);

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Id));
        }
    }
}
