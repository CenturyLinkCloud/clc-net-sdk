using CenturyLinkCloudSDK.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Integration.Tests
{
    [TestClass]
    public class BillingManagerTests
    {
        private static Client client;
        private static Authentication userAuthentication;

        [ClassInitialize]
        public static void Login(TestContext testContext)
        {
            client = new Client("mario.mamalis", "MarioTest!");
            userAuthentication = client.Authentication;
        }

        [TestMethod]
        public async Task GetAccountBillingDetailsByDataCenterListReturnValidData()
        {
            var dataCenters = await client.DataCenters.GetDataCenters().ConfigureAwait(false);
            var billingDetails = await client.BillingManager.GetBillingDetails(dataCenters).ConfigureAwait(false);
            Assert.IsTrue(billingDetails.MonthlyEstimate > 0);
        }
    }
}
