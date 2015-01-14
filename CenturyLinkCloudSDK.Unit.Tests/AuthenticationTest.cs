﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CenturyLinkCloudSDK.ServiceAPI.V2;

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
        }
    }

}