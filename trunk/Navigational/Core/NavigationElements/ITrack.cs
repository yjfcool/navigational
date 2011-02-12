using System.Collections.Generic;

namespace Core.NavigationElements
{
    /// <summary>
    /// Represents a Track - an ordered list of points describing a path.
    /// </summary>
    public interface ITrack
    {
        /// <summary>
        /// GPS name of track.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// GPS comment for track.
        /// </summary>
        string Comment { get; }
        /// <summary>
        /// User description of track.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// GPS track number.
        /// </summary>
        string Number { get; }
        /// <summary>
        /// A Track Point holds the coordinates, elevation, timestamp, and metadata for a single point in a track.
        /// </summary>
        List<ITrackSegment> TrackSegments { get; }

        string UniqueID { get; }

        /// <summary>
        /// Gets track length.
        /// </summary>
        GeoFramework.Distance Length { get; }
    }
}
