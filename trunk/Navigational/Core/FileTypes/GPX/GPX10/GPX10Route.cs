using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.FileTypes.GPX.XSD.Ver10;
using Core.NavigationElements;

namespace Core.FileTypes.GPX.GPX10
{
    class GPX10Route : IRoute
    {
        private readonly GPX.XSD.Ver10.gpxRte _rteType = null;

        private List<IWayPoint> _waypoints = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rteType"></param>
        public GPX10Route(GPX.XSD.Ver10.gpxRte rteType)
        {
            _rteType = rteType;
            _waypoints = GetRoutePoints();
        }

        #region Implementation of IRoute

        /// <summary>
        /// GPS name of route.
        /// </summary>
        public string Name
        {
            get { return _rteType.name; }
        }

        /// <summary>
        /// GPS comment for route.
        /// </summary>
        public string Comment
        {
            get { return _rteType.cmt; }
        }

        /// <summary>
        /// Text description of route for user. Not sent to GPS.
        /// </summary>
        public string Description
        {
            get { return _rteType.desc; }
        }

        /// <summary>
        /// GPS route number.
        /// </summary>
        public string Number
        {
            get { return _rteType.number; }
        }

        /// <summary>
        /// A list of route points.
        /// </summary>
        public List<IWayPoint> RoutePoints
        {
            get { return _waypoints; }
        }

        #endregion

        #region Private Methods.
        private List<IWayPoint> GetRoutePoints()
        {
            int numberOfWaypoints = _rteType.rtept.Count();
            List<IWayPoint> wayPoints = new List<IWayPoint>(numberOfWaypoints);
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                gpxWpt wpt = ConvertRouteWaypint(_rteType.rtept[i]);
                GPX10WayPoint gpxWayPoint = new GPX10WayPoint(wpt);
                wayPoints.Add(gpxWayPoint);
            }
            return wayPoints;
        }

        private gpxWpt ConvertRouteWaypint(XSD.Ver10.gpxRteRtept routePoint)
        {
            XSD.Ver10.gpxWpt wpt = new gpxWpt();
            wpt.lat = routePoint.lat;
            wpt.lon = routePoint.lon;
            wpt.ele = routePoint.ele;
            wpt.eleSpecified = routePoint.eleSpecified;
            wpt.ageofdgpsdata = routePoint.ageofdgpsdata;
            wpt.ageofdgpsdataSpecified = routePoint.ageofdgpsdataSpecified;
            wpt.Any = routePoint.Any;
            wpt.cmt = routePoint.cmt;
            wpt.desc = routePoint.desc;
            wpt.dgpsid = routePoint.dgpsid;
            wpt.fix = routePoint.fix;
            wpt.fixSpecified = routePoint.fixSpecified;
            wpt.geoidheight = routePoint.geoidheight;
            wpt.geoidheightSpecified = routePoint.geoidheightSpecified;
            wpt.hdop = routePoint.hdop;
            wpt.hdopSpecified = routePoint.hdopSpecified;
            wpt.magvar = routePoint.magvar;
            wpt.magvarSpecified = routePoint.magvarSpecified;
            wpt.name = routePoint.name;
            wpt.pdop = routePoint.pdop;
            wpt.pdopSpecified = routePoint.pdopSpecified;
            wpt.sat = routePoint.sat;
            wpt.src = routePoint.src;
            wpt.sym = routePoint.sym;
            wpt.time = routePoint.time;
            wpt.timeSpecified = routePoint.timeSpecified;
            wpt.type = routePoint.type;
            wpt.url = routePoint.url;
            wpt.urlname = routePoint.urlname;
            wpt.vdop = routePoint.vdop;
            wpt.vdopSpecified = routePoint.vdopSpecified;
            return wpt;
        }

        #endregion


    }
}
