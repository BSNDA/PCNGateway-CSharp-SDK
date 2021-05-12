using Microsoft.VisualStudio.TestTools.UnitTesting;
using bsn_sdk_csharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace bsn_sdk_csharp.Tests
{
    [TestClass()]
    public class ConfigTests
    {
        [TestMethod()]
        public void GetDefaultConfigTest()
        {
            var conf = Config.GetDefaultConfig();

            Assert.IsNotNull(conf);
        }
    }
}