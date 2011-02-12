using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Navigational.WindowsForms.NavigationalMap
{
    public interface INavigationalMapHost
    {
        void PerformOnTopPaint(Graphics graphics, Rectangle clipRectangle);
        void PerformUnderMapControlsPaint(Graphics graphics, Rectangle clipRectangle);

        void MouseClicked(int x, int y, GMap.NET.PointLatLng pointLatLng, ref bool cancel);

        void MouseDownPerformed(Point location, GMap.NET.PointLatLng pointLatLng, ref bool cancel);
        void MouseUpPerformed(Point location, GMap.NET.PointLatLng pointLatLng, ref bool cancel);
    }
}
