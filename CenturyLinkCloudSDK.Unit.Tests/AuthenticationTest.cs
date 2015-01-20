using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class AuthenticationTest
    {
        [TestMethod]
        public async Task LoginReturnTokenWhenValid()
        {
            var authenticationContext = new Authentication();
            var result = await authenticationContext.Login("mario.mamalis", "MarioTest!");

            Assert.IsNotNull(result);
            Assert.IsTrue(!String.IsNullOrEmpty(result.BearerToken));
            Assert.IsTrue(Persistence.IsUserAuthenticated);
        }

        [TestMethod]
        public async Task LoginUserNotAuthenticatedWhenInvalid()
        {
            var authenticationContext = new Authentication();
            var result = await authenticationContext.Login("mario.mamaliss", "MarioTest!");

            Assert.IsNotNull(result);
            Assert.IsTrue(String.IsNullOrEmpty(result.BearerToken));
            Assert.IsFalse(Persistence.IsUserAuthenticated);
        }
    }

}
