using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CenturyLinkCloudSDK.Runtime;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using CenturyLinkCloudSDK.ServiceModels;
using System.Linq;

namespace CenturyLinkCloudSDK.Tests
{
    [TestClass]
    public class ClientTests
    {
        static UserInfo testUserInfo =
            new UserInfo
            {
                AccountAlias = "account alias",
                BearerToken = "bearer token",
                LocationAlias = "location alias",
                UserName = "jane user",
                Roles = new[] { "role 1", "role 2" }
            };
        
        class ClientInvoker : IServiceInvoker
        {
            public Task<TResponse> Invoke<TResponse>(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
                where TResponse : class
            {
                //this can be pretty blunt for testing
                if(typeof(TResponse) == typeof(UserInfo))
                {
                    return Task<TResponse>.FromResult(testUserInfo as TResponse);
                }

                throw new Exception("ClientInvoker called with unrecognized type.");
            }
        }

        [TestInitialize]
        public void SetTestInvoker()
        {
            Configuration.ServiceInvoker = new ClientInvoker();
        }

        [TestCleanup]
        public void RestoreDefaultInvoker()
        {
            Configuration.ServiceInvoker = Configuration.DefaultServiceInvoker;
        }

        [TestMethod]
        public void CtorSetsUserInfo()
        {
            var client = new Client("username", "password");

            Assert.AreEqual(testUserInfo.AccountAlias, client.Authentication.AccountAlias);
            Assert.AreEqual(testUserInfo.BearerToken, client.Authentication.BearerToken);
            Assert.AreEqual(testUserInfo.LocationAlias, client.Authentication.LocationAlias);
            //this slightly overspecifies in that it assumes the roles are in the same order in both
            Assert.IsTrue(testUserInfo.Roles.SequenceEqual(client.Authentication.Roles));
        }
    }
}
