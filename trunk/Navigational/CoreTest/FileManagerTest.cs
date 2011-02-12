using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CoreTest
{
    /// <summary>
    /// Summary description for FileManagerTest
    /// </summary>
    [TestClass]
    public class FileManagerTest
    {
        private Core.FileManager.OpenGPSFilesManager _OpenGPSFilesManager = null;
        private const string _gpsFilePath = @"C:\Users\Public\Documents\GPX";
        private const string _exampleFile = @"C:\Users\Public\Documents\GPX\538211.gpx";

        public FileManagerTest()
        {
            _OpenGPSFilesManager = new Core.FileManager.OpenGPSFilesManager();
        }

        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Core.Globals.Configuration configuration = Core.Globals.Configuration.GetInstance();
            Core.Globals.Globals.GPSFileFolder = @"C:\Yaniv";
        }

        [TestMethod]
        public void GetStartFolderTest()
        {
            String startFolder = _OpenGPSFilesManager.GetStartFolder();
            String globalsFolder = Core.Globals.Globals.GPSFileFolder;
            Assert.AreEqual(startFolder, globalsFolder, false);
        }

        [TestMethod]
        public void SetCurrentFolderTest()
        {
            _OpenGPSFilesManager.CurrentFolder = @"C:\";
            Assert.IsTrue(string.Compare(_OpenGPSFilesManager.CurrentFolder, @"C:\") == 0);
        }

        [TestMethod]
        public void ListAllFilesTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            List<FileInfo> fileInfoCollection = _OpenGPSFilesManager.GetFileInfoList();
            Assert.IsTrue(fileInfoCollection.Count > 0);
        }

        [TestMethod]
        public void ListAllFilesByFilterTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            List<string> filters = new List<string>();
            filters.Add("*.gpx");
            filters.Add("*.txt");
            List<FileInfo> fileInfoCollection = _OpenGPSFilesManager.GetFileInfoList(filters);
            Assert.IsTrue(fileInfoCollection.Count == 8); // one .txt and two .gpx files.
        }


        [TestMethod]
        public void GetFileWaypointsCountTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            string filename = _exampleFile;
            Core.FileTypes.IFile file = _OpenGPSFilesManager.GetFile(filename);

            Assert.IsTrue(file.WayPoints.Count > 0);
        }

        [TestMethod]
        public void GetFileTracksCountTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            string filename = _exampleFile;
            Core.FileTypes.IFile file = _OpenGPSFilesManager.GetFile(filename);
            Assert.IsTrue(file.Tracks.Count > 0);
        }

        [TestMethod]
        public void GetFileTrackPointsTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            string filename = _exampleFile;
            Core.FileTypes.IFile file = _OpenGPSFilesManager.GetFile(filename);
            Assert.IsTrue(file.Tracks[0].TrackSegments[0].SegmentWaypoints.Count > 300);
        }

        [TestMethod]
        public void GetFileRoutesTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            string filename = _exampleFile;
            Core.FileTypes.IFile file = _OpenGPSFilesManager.GetFile(filename);
            Assert.IsTrue(file.Routes.Count == 0);
        }

        [TestMethod]
        public void GetFileBoundsTest()
        {
            _OpenGPSFilesManager.CurrentFolder = _gpsFilePath;
            string filename = _exampleFile;
            Core.FileTypes.IFile file = _OpenGPSFilesManager.GetFile(filename);
            Assert.IsNotNull(file.FileDetails.Bounds);
        }
    }
}
