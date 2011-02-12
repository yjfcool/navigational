using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes.GPX
{
    class GPX11Bounds:IBounds
    {
        private XSD.Ver11.boundsType _bounds = null;

        public GPX11Bounds(XSD.Ver11.boundsType bounds)
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
            get { return _bounds.minlat; }
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
