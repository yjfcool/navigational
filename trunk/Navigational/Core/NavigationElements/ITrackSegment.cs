using System.Collections.Generic;

namespace Core.NavigationElements
{
    /// <summary>
    /// A Track Segment holds a list of Track Points which are logically connected in order. To represent a single GPS track where GPS reception was lost, or the GPS receiver was turned off, start a new Track Segment for each continuous span of track data.
    /// </summary>
    public interface ITrackSegment
    {
        /// <summary>
        /// A Track Point holds the coordinates, elevation, timestamp, and metadata for a single point in a track.
        /// </summary>
        List<IWayPoint> SegmentWaypoints { get; }
    }
}
