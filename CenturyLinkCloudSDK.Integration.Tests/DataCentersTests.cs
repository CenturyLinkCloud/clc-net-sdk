using CenturyLinkCloudSDK.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class DataCentersTests
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
        public async Task GetDataCentersReturnValidData()
        {
            var result = await client.DataCenters.GetDataCenters(includeTotalAssets: false);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(d => !d.HasTotalAssets));
            Assert.IsTrue(result.ToList().Count > 0);        
        }

        [TestMethod]
        public async Task GetDataCentersIncludesAssets()
        {
            var result = await client.DataCenters.GetDataCenters(includeTotalAssets: true);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(d => d.HasTotalAssets));
            Assert.IsTrue(result.Any(d => d.Totals.Servers > 0));
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetDataCenterReturnValidData()
        {
            var result = await client.DataCenters.GetDataCenter("ca1", includeTotalAssets: false);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.HasTotalAssets);
            Assert.IsTrue(result.Id == "ca1");
        }

        [TestMethod]
        [ExpectedException(typeof(CenturyLinkCloudServiceException))]
        public async Task GetDataCenterWithBadTokenThrowException()
        {
            var client = new Client(new Authentication() { AccountAlias = "P202", BearerToken = "QEWASDADF" });
            var result = await client.DataCenters.GetDataCenter("ca1", includeTotalAssets: false);
        }

        [TestMethod]
        public async Task GetDataCenterIncludesAssets()
        {
            var result = await client.DataCenters.GetDataCenter("ca1", includeTotalAssets: true);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasTotalAssets);
            Assert.IsTrue(result.Totals.Servers > 0);
            Assert.IsTrue(result.Id == "ca1");
        }


        [TestMethod]
        public async Task GetDataCenterRootGroupReturnValidData()
        {
            var dc = await client.DataCenters.GetDataCenter("ca1", includeTotalAssets: false);

            var rootGroup = await dc.GetRootGroup().ConfigureAwait(false);

            Assert.IsNotNull(rootGroup);
            Assert.IsTrue(!string.IsNullOrEmpty(rootGroup.Name));            
        }

        [TestMethod]
        public async Task GetDataCenterOverview()
        {
            var dataCenterOverview = await client.DataCenters.GetDataCenterOverview("ca1").ConfigureAwait(false);

            Assert.IsTrue(dataCenterOverview.DataCenter.Id == "ca1");
            Assert.IsTrue(dataCenterOverview.BillingTotals.MonthlyEstimate > 0);
            Assert.IsTrue(dataCenterOverview.ComputeLimits.StorageGB.Value > 0);
            Assert.IsTrue(dataCenterOverview.DataCenter.Totals.StorageGB > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(dataCenterOverview.DefaultSettings.PrimaryDns.Value));
            Assert.IsTrue(dataCenterOverview.RecentActivity.Count() > 0);
            Assert.IsTrue(dataCenterOverview.NetworkLimits.Networks.Value > 0);
        }

        /*
        [TestMethod]
        public async Task GetDataCenterDeploymentCapabilities()
        {
            var result = await client.DataCenters.GetDeploymentCapabilities("ca1").ConfigureAwait(false);

            Assert.IsTrue(result.DataCenterEnabled);
            Assert.IsTrue(result.Templates.Count() > 0);
        }
         */
    }
}
