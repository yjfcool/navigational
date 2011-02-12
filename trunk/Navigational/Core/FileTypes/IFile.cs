using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes
{
    public interface IFile
    {
        /// <summary>
        /// Gets file version.
        /// </summary>
        string FileVersion { get; }
        /// <summary>
        /// Gets file type.
        /// </summary>
        string FileType { get; }
        /// <summary>
        /// Gets the gps filename.
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Gets if parsing the file was completed succesfully.
        /// </summary>
        bool IsFileValid { get; }
        /// <summary>
        /// Gets file's details.
        /// </summary>
        IFileDetails FileDetails { get; }
        /// <summary>
        /// Gets a list of the gps file's tracks.
        /// </summary>
        List<ITrack> Tracks { get; }
        /// <summary>
        /// Gets a list of the gps file's routes.
        /// </summary>
        List<IRoute> Routes { get; }
        /// <summary>
        /// Gets a list of the gps file's waypoints.
        /// </summary>
        List<IWayPoint> WayPoints { get; }
    }
}
