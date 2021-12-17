using Microsoft.VisualStudio.TestTools.UnitTesting;

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