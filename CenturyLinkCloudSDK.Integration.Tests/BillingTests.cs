using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceModels;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class BillingTests
    {        
        private static Client client;
        private static Authentication authentication;

        [ClassInitialize]
        public static void Login(TestContext testContext)
        {
            client = new Client(Credentials.Username, Credentials.Password);
            authentication = client.Authentication;
        }

        [TestMethod]
        public async Task GetAccountBillingDetailsReturnValidData()
        {
            var result = await client.Billing.GetBillingDetails().ConfigureAwait(false);
            Assert.IsTrue(result.Total.MonthlyEstimate > 0);
            Assert.IsTrue(result.Total.MonthToDate > 0);
            Assert.IsTrue(result.Total.CurrentHour > 0);
        }

        [TestMethod]
        public async Task GetDataCenterBillingDetailsReturnValidData()
        {
            var dataCenter = await client.DataCenters.GetDataCenter("CA1", includeTotalAssets: false).ConfigureAwait(false);
            var result = await client.Billing.GetBillingDetailsFor(dataCenter).ConfigureAwait(false);
            Assert.IsTrue(result.MonthlyEstimate > 0);
        }

        [TestMethod]
        public async Task GetGroupBillingDetailsReturnValidData()
        {
            var group = await client.Groups.GetGroup("a726bd9f7d9be411877f005056882d41").ConfigureAwait(false);
            var result = await client.Billing.GetBillingDetailsFor(group).ConfigureAwait(false);
            Assert.IsTrue(result.MonthlyEstimate > 0);
        }

        [TestMethod]
        public async Task GetServerBillingDetailsReturnValidData()
        {
            var server = await client.Servers.GetServer("CA1P2O2DF2TST01").ConfigureAwait(false);
            var result = await client.Billing.GetBillingDetailsFor(server).ConfigureAwait(false);
            Assert.IsTrue(result.MonthlyEstimate > 0);
        }

        [TestMethod]
        public async Task GetServerResourceUnitPricingReturnValidData()
        {
            var result = await client.Billing.GetServerResourceUnitPricing("ca1p2o2df2tst01").ConfigureAwait(false);
            Assert.IsTrue(result.MemoryGB > 0);
        }
    }
}
