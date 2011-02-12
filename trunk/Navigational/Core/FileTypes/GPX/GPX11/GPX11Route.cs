using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes.GPX
{
    class GPX11Route : IRoute
    {
        private readonly GPX.XSD.Ver11.rteType _rteType = null;

        private List<IWayPoint> _waypoints = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rteType"></param>
        public GPX11Route(XSD.Ver11.rteType rteType)
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
                GPX11WayPoint gpxWayPoint = new GPX11WayPoint(_rteType.rtept[i]);
                wayPoints.Add(gpxWayPoint);
            }
            return wayPoints;
        } 
        #endregion
    }
}