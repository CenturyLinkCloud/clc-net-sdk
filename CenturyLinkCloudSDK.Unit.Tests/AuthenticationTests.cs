using CenturyLinkCloudSDK.ServiceAPI.Runtime;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class AuthenticationTests
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

            Assert.IsNull(result);
        }
    }

}
