using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CenturyLinkCloudSDK.ServiceAPI.V2;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceAPI.Runtime;

namespace CenturyLinkCloudSDK.Unit.Tests
{
    [TestClass]
    public class ServerTest
    {
        [TestInitialize]
        public void Login()
        {
            var authentication = new Authentication();
            var result = authentication.Login("mario.mamalis", "MarioTest!").Result;
        }

        [TestMethod]
        public async Task GetServerReturnValidData()
        {
            var serverContext = new Server();
            var result = await serverContext.GetServer(Persistence.UserInfo.AccountAlias, "CA1P2O2DF2TST01");  //VA1P2O2JAMMU01

            Assert.IsNotNull(result);
            Assert.IsTrue(!String.IsNullOrEmpty(result.Id));

        }
    }
}
