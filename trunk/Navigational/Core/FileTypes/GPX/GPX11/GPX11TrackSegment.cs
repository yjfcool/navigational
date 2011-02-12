using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes.GPX
{
    class GPX11TrackSegment : ITrackSegment
    {
        private XSD.Ver11.trksegType _trkSeg = null;
        private List<IWayPoint> _segmentWaypoints = null;

        public GPX11TrackSegment(XSD.Ver11.trksegType trkSeg)
        {
            _trkSeg = trkSeg;
            _segmentWaypoints = GetSegmentWaypoints();
        }

        #region Implementation of ITrackSegment

        /// <summary>
        /// A Track Point holds the coordinates, elevation, timestamp, and metadata for a single point in a track.
        /// </summary>
        public List<IWayPoint> SegmentWaypoints
        {
            get { return _segmentWaypoints; }
        }

        #endregion

        private List<IWayPoint> GetSegmentWaypoints()
        {
            int numberOfTrackPoints = _trkSeg.trkpt.Count();
            List<IWayPoint> wayPoints = new List<IWayPoint>(numberOfTrackPoints);

            for (int i = 0; i < numberOfTrackPoints; i++)
            {
                IWayPoint wayPoint = new GPX11WayPoint(_trkSeg.trkpt[i]);
                wayPoints.Add(wayPoint);
            }

            return wayPoints;
        }
    }
}
