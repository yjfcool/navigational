namespace Core.NavigationElements
{
    /// <summary>
    /// Represents a waypoint, point of interest, or named feature on a map.
    /// </summary>
    public interface IWayPoint
    {
        /// <summary>
        /// Elevation (in meters) of the point.
        /// </summary>
        decimal Elevation { get; }
        /// <summary>
        /// Creation/modification timestamp for element. Date and time in are in Univeral Coordinated Time (UTC), not local time! Conforms to ISO 8601 specification for date/time representation. Fractional seconds are allowed for millisecond timing in tracklogs.
        /// </summary>
        System.DateTime Time { get; }
        /// <summary>
        /// Magnetic variation (in degrees) at the point
        /// </summary>
        decimal MagneticVariation { get; }
        /// <summary>
        /// Height (in meters) of geoid (mean sea level) above WGS84 earth ellipsoid. As defined in NMEA GGA message.
        /// </summary>
        decimal GeoidHeight { get; }
        /// <summary>
        /// The GPS name of the waypoint. This field will be transferred to and from the GPS.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// GPS waypoint comment. Sent to GPS as comment.
        /// </summary>
        string Comment { get; }
        /// <summary>
        /// A text description of the element. Holds additional information about the element intended for the user, not the GPS.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Horizontal dilution of precision.
        /// </summary>
        decimal HDOP { get; }
        /// <summary>
        /// Vertical dilution of precision.
        /// </summary>
        decimal VDOP { get; }
        /// <summary>
        /// Position dilution of precision.
        /// </summary>
        decimal PDOP { get; }
        /// <summary>
        /// The latitude of the point. Decimal degrees, WGS84 datum.
        /// </summary>
        decimal Latitude { get; }
        /// <summary>
        /// The longitude of the point. Decimal degrees, WGS84 datum.
        /// </summary>
        decimal Longitude { get; }

        /// <summary>
        /// Manually sets waypoint position.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        void SetPosition(decimal latitude, decimal longitude);
    }
}
