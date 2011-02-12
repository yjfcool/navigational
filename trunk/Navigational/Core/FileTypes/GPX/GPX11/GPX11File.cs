using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Core.FileTypes.GPX.XSD.Ver11;
using Core.NavigationElements;


namespace Core.FileTypes.GPX
{
    /// <summary>
    /// Represents a GPX version 1.1 file.
    /// </summary>
    class GPX11File : IFile
    {
        private readonly string _filename = string.Empty;
        private readonly gpxType _gpx11 = null;
        private readonly bool _isFileValid = true;
        private readonly IFileDetails _gpxFileDetails = null;

        private List<IRoute> _routes = null;
        private List<ITrack> _tracks = null;
        private List<IWayPoint> _waypoints = null;

        #region Constructors and Initializers.
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filename"></param>
        public GPX11File(string filename)
        {
            try
            {
                _filename = filename;
                _gpx11 = DeserializeGPX();
                _gpxFileDetails = new GPX11FileDetails(_gpx11);

                _routes = GetRoutes();
                _tracks = GetTracks();
                _waypoints = GetWayPoints();
            }
            catch (Exception)
            {
                _isFileValid = false;
            }
        }

        private XSD.Ver11.gpxType DeserializeGPX()
        {
            string xmlString = GetXMLFromFile();
            object retVal = null;

            XmlSerializer serializer = new XmlSerializer(typeof(gpxType));
            StringReader stringReader = new StringReader(xmlString);
            XmlTextReader xmlReader = new XmlTextReader(stringReader);

            retVal = serializer.Deserialize(xmlReader);
            return retVal as gpxType;
        }

        private string GetXMLFromFile()
        {
            string xmlString = string.Empty;

            if (File.Exists(_filename))
            {
                xmlString = File.ReadAllText(_filename);
            }
            return xmlString;
        }
        #endregion

        #region IFile Members

        /// <summary>
        /// Gets file version.
        /// </summary>
        public string FileVersion
        {
            get { return "1.1"; }
        }

        /// <summary>
        /// Gets file type.
        /// </summary>
        public string FileType
        {
            get { return "GPX"; }
        }

        /// <summary>
        /// Gets the gps filename.
        /// </summary>
        public string FileName
        {
            get { return _filename; }
        }

        /// <summary>
        /// Gets if parsing the file was completed succesfully.
        /// </summary>
        public bool IsFileValid
        {
            get { return _isFileValid; }
        }

        /// <summary>
        /// Gets file's details.
        /// </summary>
        public IFileDetails FileDetails
        {
            get { return _gpxFileDetails; }
        }

        /// <summary>
        /// Gets a list of the gps file's tracks.
        /// </summary>
        public List<ITrack> Tracks
        {
            get { return _tracks; }
        }

        /// <summary>
        /// Gets a list of the gps file's routes.
        /// </summary>
        public List<IRoute> Routes
        {
            get { return _routes; }
        }

        /// <summary>
        /// Gets a list of the gps file's waypoints.
        /// </summary>
        public List<IWayPoint> WayPoints
        {
            get { return _waypoints; }
        }

        #endregion

        private List<IWayPoint> GetWayPoints()
        {
            if (_gpx11.wpt == null)
            {
                return new List<IWayPoint>(0);
            }

            int numberOfWaypoints = _gpx11.wpt.Count();
            List<IWayPoint> wayPoints = new List<IWayPoint>(numberOfWaypoints);
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                GPX11WayPoint gpxWayPoint = new GPX11WayPoint(_gpx11.wpt[i]);
                wayPoints.Add(gpxWayPoint);
            }
            return wayPoints;
        }

        private List<IRoute> GetRoutes()
        {
            if (_gpx11.rte == null)
            {
                return new List<IRoute>(0);
            }
            int numberOfRoutes = _gpx11.rte.Count();
            List<IRoute> routes = new List<IRoute>(numberOfRoutes);
            for (int i = 0; i < numberOfRoutes; i++)
            {
                GPX11Route gpxRoute = new GPX11Route(_gpx11.rte[i]);
                routes.Add(gpxRoute);
            }
            return routes;
        }

        private List<ITrack> GetTracks()
        {
            if (_gpx11.trk == null)
            {
                return new List<ITrack>(0);
            }
            int numberOfTracks = _gpx11.trk.Count();
            List<ITrack> tracks = new List<ITrack>(numberOfTracks);
            for (int i = 0; i < numberOfTracks; i++)
            {
                GPX11Track gpxTrack = new GPX11Track(_gpx11.trk[i]);
                tracks.Add(gpxTrack);
            }
            return tracks;
        }
    }
}
