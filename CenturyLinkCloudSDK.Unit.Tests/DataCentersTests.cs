using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class DataCentersTests
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
        public async Task GetDataCenterGroupReturnValidData()
        {
            var dataCenterContext = new DataCenters();
            var result = await dataCenterContext.GetDataCenterGroup(Persistence.UserInfo.AccountAlias, "ca1");

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Id));
            Assert.IsTrue(result.Id == "ca1");
        }

        [TestMethod]
        public async Task GetDataCenterGroupByHyperlinkReturnValidData()
        {
            var dataCenterContext = new DataCenters();
            var result = await dataCenterContext.GetDataCenterGroup("/v2/datacenters/p2o2/ca1");

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Id));
            Assert.IsTrue(result.Id == "ca1");
        }
    }
}
