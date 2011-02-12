using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    [TestClass]
    public class GlobalsConfigurationTest
    {
        [TestMethod]
        public void InitGlobalConfigurationTest()
        {
            Core.Globals.Configuration configuration = Core.Globals.Configuration.GetInstance();
            Assert.IsNotNull(configuration);
        }

        [TestMethod]
        public void GlobalConfigurationSaveTest()
        {
            Core.Globals.Configuration configuration = Core.Globals.Configuration.GetInstance();

            configuration.GlobalConfiguration.OpenGPSFilePath = "Yaniv 4";
            configuration.SaveConfiguration();

            Assert.IsTrue(string.Compare(configuration.GlobalConfiguration.OpenGPSFilePath, "Yaniv 4") == 0);
        }
    }
}
