using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.FileTypes.GPX.XSD.Ver10;
using Core.NavigationElements;

namespace Core.FileTypes.GPX.GPX10
{
    class GPX10TrackSegment : ITrackSegment
    {

        private XSD.Ver10.gpxTrkTrkseg _trkSeg = null;

        private List<IWayPoint> _segmentWaypoints = null;

        public GPX10TrackSegment(XSD.Ver10.gpxTrkTrkseg trkSeg)
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
            int numberOfTrackPoints = _trkSeg.trkpnt.Length;
            List<IWayPoint> wayPoints = new List<IWayPoint>(numberOfTrackPoints);

            for (int i = 0; i < numberOfTrackPoints; i++)
            {
                IWayPoint wayPoint = new GPX10WayPoint(ConvertToWayPoint(_trkSeg.trkpnt[i]));
                wayPoints.Add(wayPoint);
            }

            return wayPoints;
        }

        private gpxWpt ConvertToWayPoint(XSD.Ver10.gpxTrkTrksegTrkpt trackSegmentWaypoint)
        {
            XSD.Ver10.gpxWpt wpt = new gpxWpt();
            wpt.lat = trackSegmentWaypoint.lat;
            wpt.lon = trackSegmentWaypoint.lon;
            wpt.ele = trackSegmentWaypoint.ele;
            wpt.eleSpecified = trackSegmentWaypoint.eleSpecified;
            wpt.ageofdgpsdata = trackSegmentWaypoint.ageofdgpsdata;
            wpt.ageofdgpsdataSpecified = trackSegmentWaypoint.ageofdgpsdataSpecified;
            wpt.Any = trackSegmentWaypoint.Any;
            wpt.cmt = trackSegmentWaypoint.cmt;
            wpt.desc = trackSegmentWaypoint.desc;
            wpt.dgpsid = trackSegmentWaypoint.dgpsid;
            wpt.fix = trackSegmentWaypoint.fix;
            wpt.fixSpecified = trackSegmentWaypoint.fixSpecified;
            wpt.geoidheight = trackSegmentWaypoint.geoidheight;
            wpt.geoidheightSpecified = trackSegmentWaypoint.geoidheightSpecified;
            wpt.hdop = trackSegmentWaypoint.hdop;
            wpt.hdopSpecified = trackSegmentWaypoint.hdopSpecified;
            wpt.magvar = trackSegmentWaypoint.magvar;
            wpt.magvarSpecified = trackSegmentWaypoint.magvarSpecified;
            wpt.name = trackSegmentWaypoint.name;
            wpt.pdop = trackSegmentWaypoint.pdop;
            wpt.pdopSpecified = trackSegmentWaypoint.pdopSpecified;
            wpt.sat = trackSegmentWaypoint.sat;
            wpt.src = trackSegmentWaypoint.src;
            wpt.sym = trackSegmentWaypoint.sym;
            wpt.time = trackSegmentWaypoint.time;
            wpt.timeSpecified = trackSegmentWaypoint.timeSpecified;
            wpt.type = trackSegmentWaypoint.type;
            wpt.url = trackSegmentWaypoint.url;
            wpt.urlname = trackSegmentWaypoint.urlname;
            wpt.vdop = trackSegmentWaypoint.vdop;
            wpt.vdopSpecified = trackSegmentWaypoint.vdopSpecified;
            return wpt;
        }
    }
}
