using System.Collections.Generic;

namespace Core.NavigationElements
{
    /// <summary>
    /// Represents Route - an ordered list of waypoints representing a series of turn points leading to a destination.
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// GPS name of route.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// GPS comment for route.
        /// </summary>
        string Comment { get; }
        /// <summary>
        /// Text description of route for user. Not sent to GPS.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// GPS route number.
        /// </summary>
        string Number { get; }
        /// <summary>
        /// A list of route points.
        /// </summary>
        List<IWayPoint> RoutePoints { get; }
    }
}
