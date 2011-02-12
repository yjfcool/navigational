using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;

namespace Navigational.WindowsForms.NavigationalControls.TrackEditor.Markers
{
    class GhostMarker : GMapMarker
    {
        Core.NavigationElements.IWayPoint _wayPoint = null;

        public GhostMarker(GMap.NET.PointLatLng p)
            : base(p)
        {

            base.Size = new Size(8, 8);
            Offset = new Point(-4, -4);
        }


        public override void OnRender(Graphics g)
        {
            base.OnRender(g);
            g.FillEllipse(Brushes.Blue, LocalPosition.X + 1, LocalPosition.Y + 1, 6, 6);
            g.DrawEllipse(Pens.Black, LocalPosition.X, LocalPosition.Y, 8, 8);
        }
    }
}
