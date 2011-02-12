using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Navigational.WindowsForms.NavigationalControls.TrackEditor.Markers
{
    class TrackPoint : GMap.NET.WindowsForms.GMapMarker
    {
        private int _trackIndex = -1;
        private GMapRoute _route = null;

        public TrackPoint(int trackIndex, PointLatLng point, GMap.NET.WindowsForms.GMapRoute relatedRoute)
            : base(point)
        {
            _trackIndex = trackIndex;
            _route = relatedRoute;
        }
    }
}
