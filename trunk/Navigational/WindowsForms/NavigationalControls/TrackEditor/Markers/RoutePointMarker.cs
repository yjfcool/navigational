using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;

namespace Navigational.WindowsForms.NavigationalControls.TrackEditor.Markers
{
    class RoutePointMarker : GMapMarker
    {
        private int _routePointIndex = -1;
        private GMapRoute _route = null;

        public int RoutePointIndex
        {
            get { return _routePointIndex; }
        }
        public GMapRoute Route
        {
            get { return _route; }
        }

        public RoutePointMarker(GMap.NET.PointLatLng p, int routePointIndex, GMapRoute route)
            : base(p)
        {
            _route = route;
            _routePointIndex = routePointIndex;
            base.Size = new Size(8, 8);
            base.Offset = new Point((-1) * base.Size.Width / 2, (-1) * base.Size.Height / 2);
        }

        /// <summary>
        /// Sets route index.
        /// </summary>
        /// <param name="newIndex"></param>
        public void SetRouteIndex(int newIndex)
        {
            _routePointIndex = newIndex;
        }

        public override void OnRender(Graphics g)
        {
            base.OnRender(g);
            g.FillEllipse(Brushes.Red, LocalPosition.X + 1, LocalPosition.Y + 1, 6, 6);
            g.DrawEllipse(Pens.Black, LocalPosition.X, LocalPosition.Y, 8, 8);
        }


    }
}
