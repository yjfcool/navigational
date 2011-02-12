using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;
using System.Security.Cryptography;
using GeoFramework;

namespace Core.FileTypes.GPX
{
    class GPX11Track : ITrack
    {
        private readonly GPX.XSD.Ver11.trkType _trkType = null;
        private readonly string _uniqueID = string.Empty;

        private Distance _trackLength = GeoFramework.Distance.Empty;

        private List<ITrackSegment> _trackSegments = null;

        public GPX11Track(XSD.Ver11.trkType trkType)
        {
            _trkType = trkType;
            _uniqueID = GetUniqueKey();

            _trackSegments = GetTrackSegments();
        }

        private string GetUniqueKey()
        {
            return Services.GenerateUniqueInt().ToString();


            #region Legacy.
            //int maxSize = 8;
            //int minSize = 5;
            //char[] chars = new char[62];
            //string a;
            //a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            //chars = a.ToCharArray();
            //int size = maxSize;
            //byte[] data = new byte[1];
            //RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            //crypto.GetNonZeroBytes(data);
            //size = maxSize;
            //data = new byte[size];
            //crypto.GetNonZeroBytes(data);
            //StringBuilder result = new StringBuilder(size);
            //foreach (byte b in data)
            //{ result.Append(chars[b % (chars.Length - 1)]); }
            //return result.ToString(); 
            #endregion
        }

        #region Implementation of ITrack

        /// <summary>
        /// GPS name of track.
        /// </summary>
        public string Name
        {
            get { return _trkType.name; }
        }

        /// <summary>
        /// GPS comment for track.
        /// </summary>
        public string Comment
        {
            get { return _trkType.cmt; }
        }

        /// <summary>
        /// User description of track.
        /// </summary>
        public string Description
        {
            get { return _trkType.desc; }
        }

        /// <summary>
        /// GPS track number.
        /// </summary>
        public string Number
        {
            get { return _trkType.number; }
        }

        /// <summary>
        /// A Track Point holds the coordinates, elevation, timestamp, and metadata for a single point in a track.
        /// </summary>
        public List<ITrackSegment> TrackSegments
        {
            get { return _trackSegments; }
        }

        public string UniqueID
        {
            get { return _uniqueID; }
        }

        #endregion

        private List<ITrackSegment> GetTrackSegments()
        {
            int numberOfSegments = _trkType.trkseg.Count();
            List<ITrackSegment> trackSegments = new List<ITrackSegment>(numberOfSegments);

            for (int i = 0; i < numberOfSegments; i++)
            {
                ITrackSegment trackSegment = new GPX11TrackSegment(_trkType.trkseg[i]);
                trackSegments.Add(trackSegment);
            }
            return trackSegments;
        }


        public GeoFramework.Distance Length
        {
            get
            {
                if (_trackLength == GeoFramework.Distance.Empty)
                {
                    GeoFramework.Distance totalDistance = CalculateTrackLength();

                    _trackLength = totalDistance;
                }

                return _trackLength;
            }
        }

        private Distance CalculateTrackLength()
        {
            Position pos = GeoFramework.Position.Empty;
            Distance totalDistance = new GeoFramework.Distance();

            for (int i = 0; i < _trackSegments.Count; i++)
            {
                for (int j = 0; j < _trackSegments[i].SegmentWaypoints.Count; j++)
                {
                    Position current = GeoFramework.Position.Empty;
                    Latitude lat = new GeoFramework.Latitude((double)_trackSegments[i].SegmentWaypoints[j].Latitude);
                    Longitude lon = new GeoFramework.Longitude((double)_trackSegments[i].SegmentWaypoints[j].Longitude);

                    if (pos == GeoFramework.Position.Empty)
                    {
                        pos = new GeoFramework.Position(lon, lat);
                    }
                    else
                    {
                        current = new GeoFramework.Position(lon, lat);
                        Distance distance = current.DistanceTo(pos);
                        totalDistance += distance;
                        pos = current;
                    }
                }
            }
            return totalDistance;
        }

    }
}
