using CenturyLinkCloudSDK.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.IntegrationTests
{
    [TestClass]
    public class AlertTests
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
        public async Task GetAlertPoliciesForAccountReturnValidData()
        {
            var alertPolicies = await client.Alerts.GetAlertPolicies().ConfigureAwait(false);
            Assert.IsTrue(alertPolicies.Items.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetServerAlerts()
        {
            var alerts = await client.Alerts.GetAllServerAlerts().ConfigureAwait(false);
            Assert.IsTrue(alerts.ToList().Count > 0);
        }

        [TestMethod]
        public async Task GetServerAlertsForOneServer()
        {
            var server = await client.Servers.GetServer("ca1p2o2df2tst01").ConfigureAwait(false);
            var alerts = await client.Alerts.GetServerAlerts(server).ConfigureAwait(false);
            Assert.IsTrue(alerts.ToList().Count > 0);
        }        
    }
}
