using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Core.FileTypes.GPX.XSD.Ver10;
using Core.NavigationElements;

namespace Core.FileTypes.GPX.GPX10
{
    class GPX10File : IFile
    {

        private readonly string _filename = string.Empty;
        private readonly gpx _gpx10 = null;
        private readonly bool _isFileValid = true;
        private readonly IFileDetails _gpxFileDetails = null;

        private List<IRoute> _routes = null;
        private List<ITrack> _tracks = null;
        private List<IWayPoint> _waypoints = null;

        public GPX10File(string filename)
        {
            try
            {
                _filename = filename;
                _gpx10 = DeserializeGPX();
                _gpxFileDetails = new GPX10FileDetails(_gpx10);

                _routes = GetRoutes();
                _tracks = GetTracks();
                _waypoints = GetWayPoints();
            }
            catch (Exception)
            {
                _isFileValid = false;
            }
        }


        private gpx DeserializeGPX()
        {
            string xmlString = GetXMLFromFile();
            object retVal = null;

            XmlSerializer serializer = new XmlSerializer(typeof(gpx));
            StringReader stringReader = new StringReader(xmlString);
            XmlTextReader xmlReader = new XmlTextReader(stringReader);

            retVal = serializer.Deserialize(xmlReader);
            return retVal as gpx;
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

        #region IFile Members

        /// <summary>
        /// Gets file version.
        /// </summary>
        public string FileVersion
        {
            get { return "1.0"; }
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
            if (_gpx10.wpt == null)
            {
                return new List<IWayPoint>(0);
            }

            int numberOfWaypoints = _gpx10.wpt.Count();
            List<IWayPoint> wayPoints = new List<IWayPoint>(numberOfWaypoints);
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                GPX10WayPoint gpxWayPoint = new GPX10WayPoint(_gpx10.wpt[i]);
                wayPoints.Add(gpxWayPoint);
            }
            return wayPoints;
        }

        private List<IRoute> GetRoutes()
        {
            if (_gpx10.rte == null)
            {
                return new List<IRoute>(0);
            }
            int numberOfRoutes = _gpx10.rte.Count();
            List<IRoute> routes = new List<IRoute>(numberOfRoutes);
            for (int i = 0; i < numberOfRoutes; i++)
            {
                var gpxRoute = new GPX10Route(_gpx10.rte[i]);
                routes.Add(gpxRoute);
            }
            return routes;
        }

        private List<ITrack> GetTracks()
        {
            if (_gpx10.trk == null)
            {
                return new List<ITrack>(0);
            }
            int numberOfTracks = _gpx10.trk.Count();
            List<ITrack> tracks = new List<ITrack>(numberOfTracks);
            for (int i = 0; i < numberOfTracks; i++)
            {
                var gpxTrack = new GPX10Track(_gpx10.trk[i]);
                tracks.Add(gpxTrack);
            }
            return tracks;
        }
    }
}
