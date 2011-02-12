using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes.GPX
{
    class GPX11WayPoint : IWayPoint
    {
        private readonly GPX.XSD.Ver11.wptType _wptType;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wptType"></param>
        public GPX11WayPoint(GPX.XSD.Ver11.wptType wptType)
        {
            _wptType = wptType;
        }

        #region Implementation of IWayPoint

        /// <summary>
        /// Elevation (in meters) of the point.
        /// </summary>
        public decimal Elevation
        {
            get
            {
                if (_wptType.eleSpecified)
                {
                    return _wptType.ele;
                }
                return decimal.Zero;
            }
        }

        /// <summary>
        /// Creation/modification timestamp for element. Date and time in are in Univeral Coordinated Time (UTC), not local time! Conforms to ISO 8601 specification for date/time representation. Fractional seconds are allowed for millisecond timing in tracklogs.
        /// </summary>
        public DateTime Time
        {
            get
            {
                if (_wptType.timeSpecified) return _wptType.time;
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Magnetic variation (in degrees) at the point
        /// </summary>
        public decimal MagneticVariation
        {
            get
            {
                if (_wptType.magvarSpecified) return _wptType.magvar;
                return decimal.Zero;
            }
        }
        /// <summary>
        /// Height (in meters) of geoid (mean sea level) above WGS84 earth ellipsoid. As defined in NMEA GGA message.
        /// </summary>
        public decimal GeoidHeight
        {
            get
            {
                if (_wptType.geoidheightSpecified) return _wptType.geoidheight;
                return decimal.Zero;
            }
        }
        /// <summary>
        /// The GPS name of the waypoint. This field will be transferred to and from the GPS.
        /// </summary>
        public string Name
        {
            get { return _wptType.name; }
        }
        /// <summary>
        /// GPS waypoint comment. Sent to GPS as comment.
        /// </summary>
        public string Comment
        {
            get { return _wptType.cmt; }
        }
        /// <summary>
        /// A text description of the element. Holds additional information about the element intended for the user, not the GPS.
        /// </summary>
        public string Description
        {
            get { return _wptType.desc; }
        }

        /// <summary>
        /// Horizontal dilution of precision.
        /// </summary>
        public decimal HDOP
        {
            get
            {
                if (_wptType.hdopSpecified) return HDOP;
                return decimal.Zero;
            }
        }
        /// <summary>
        /// Vertical dilution of precision.
        /// </summary>
        public decimal VDOP
        {
            get
            {
                if (_wptType.vdopSpecified) return VDOP;
                return decimal.Zero;
            }
        }
        /// <summary>
        /// Position dilution of precision.
        /// </summary>
        public decimal PDOP
        {
            get
            {
                if (_wptType.pdopSpecified) return PDOP;
                return decimal.Zero;
            }
        }
        /// <summary>
        /// The latitude of the point. Decimal degrees, WGS84 datum.
        /// </summary>
        public decimal Latitude
        {
            get { return _wptType.lat; }
        }
        /// <summary>
        /// The longitude of the point. Decimal degrees, WGS84 datum.
        /// </summary>
        public decimal Longitude
        {
            get { return _wptType.lon; }
        }

        #endregion


        public void SetPosition(decimal latitude, decimal longitude)
        {
            _wptType.lat = latitude;
            _wptType.lon = longitude;
        }
    }
}
