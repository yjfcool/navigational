using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;

namespace Navigational.WindowsForms.NavigationalControls.TrackEditor.Markers
{
    class GhostPoint
    {
        private int _indexPoint;
        private GMapMarker _marker;

        #region Properties.
        /// <summary>
        /// Gets previous point index.
        /// </summary>
        public int IndexPoint
        {
            get { return _indexPoint; }
        }

        public GMapMarker Marker
        {
            get { return _marker; }
        }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="indexPoint"></param>
        /// <param name="nextPoint"></param>
        public GhostPoint(int indexPoint, GMapMarker marker)
        {
            _indexPoint = indexPoint;
            _marker = marker;
        }

        public static GhostPoint operator ++(GhostPoint ghostPoint)
        {
            ghostPoint._indexPoint++;
            return ghostPoint;
        }

        /// <summary>
        /// Sets the index point value.
        /// </summary>
        /// <param name="index"></param>
        public void SetIndexPoint(int index)
        {
            _indexPoint = index;
        }
    }
}
