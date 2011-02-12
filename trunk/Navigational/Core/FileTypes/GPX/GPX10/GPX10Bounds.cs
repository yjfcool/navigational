using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.FileTypes.GPX.XSD.Ver10;

namespace Core.FileTypes.GPX.GPX10
{
    class GPX10Bounds:Core.NavigationElements.IBounds
    {
        private XSD.Ver10.boundsType _bounds = null;

        public GPX10Bounds(XSD.Ver10.boundsType bounds)
        {
            _bounds = bounds;
        }

        #region Implementation of IBounds

        /// <summary>
        /// The minimum latitude.
        /// </summary>
        public decimal MinLatitude
        {
            get { return _bounds.minlat; }
        }

        /// <summary>
        /// The minimum longitude.
        /// </summary>
        public decimal MinLongitude
        {
            get { return _bounds.minlon; }
        }

        /// <summary>
        /// The maximum latitude.
        /// </summary>
        public decimal MaxLatitude
        {
            get { return _bounds.maxlat; }
        }

        /// <summary>
        /// The maximum longitude.
        /// </summary>
        public decimal MaxLongitude
        {
            get { return _bounds.maxlon; }
        }

        #endregion
    }
}
