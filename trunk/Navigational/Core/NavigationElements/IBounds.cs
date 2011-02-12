namespace Core.NavigationElements
{
    /// <summary>
    /// Two lat/lon pairs defining the extent of an element.
    /// </summary>
    public interface IBounds
    {
        /// <summary>
        /// The minimum latitude.
        /// </summary>
        decimal MinLatitude { get; }
        /// <summary>
        /// The minimum longitude.
        /// </summary>
        decimal MinLongitude { get; }
        /// <summary>
        /// The maximum latitude.
        /// </summary>
        decimal MaxLatitude { get; }
        /// <summary>
        /// The maximum longitude.
        /// </summary>
        decimal MaxLongitude { get; }
    }
}
