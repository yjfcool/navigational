using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Core.ElementsOrginizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    [TestClass]
    public class ElementsOrginizerTest
    {
        [TestMethod]
        public void InitializeElementsOrginizerTest()
        {
            Assert.IsNotNull(ElementsOrginizer.Instance);
        }

        [TestMethod]
        public void AddFilesTest()
        {
            var openGPSFilesManager = new Core.FileManager.OpenGPSFilesManager();
            var file1 = openGPSFilesManager.GetFile(@"C:\Users\yaniv\Desktop\Tracks\Baticha_To_Banias2.gpx");

            ElementsOrginizer.Instance.AddFile(file1);

            
        }

    }
}
