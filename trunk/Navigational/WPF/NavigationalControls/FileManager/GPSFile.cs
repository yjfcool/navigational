using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Navigational.WPF.NavigationalControls.FileManager
{
    internal class GPSFile
    {
        readonly Core.FileTypes.IFile _gpsFile = null;

        #region Properties.
        /// <summary>
        /// Gets gps filename.
        /// </summary>
        public string FileName
        {
            get
            {
                return Path.GetFileName(_gpsFile.FileName);
            }
        }

        /// <summary>
        /// Gets gps file description.
        /// </summary>
        public string Description
        {
            get
            {
                string desc = _gpsFile.FileDetails.Description;
                if (string.IsNullOrEmpty(desc))
                {
                    desc = "No Description";
                }
                return desc;
            }
        }
        /// <summary>
        /// Gets gps file tracks count.
        /// </summary>
        public int TracksCount
        {
            get { return _gpsFile.Tracks.Count; }
        }

        /// <summary>
        /// Gets gps file routes count.
        /// </summary>
        public int RoutesCount
        {
            get
            {
                return _gpsFile.Routes.Count;
            }
        }
        /// <summary>
        /// Gets gps file waypoints counts.
        /// </summary>
        public int WaypointsCount
        {
            get
            {
                return _gpsFile.WayPoints.Count;
            }
        }

        /// <summary>
        /// Gets gps file date.
        /// </summary>
        public DateTime Date
        {
            get { return _gpsFile.FileDetails.Date; }
        }
        /// <summary>
        /// Gets file type.
        /// </summary>
        public string FileType
        {
            get { return _gpsFile.FileType; }
        }

        /// <summary>
        /// Gets file version.
        /// </summary>
        public string FileVersion
        {
            get { return _gpsFile.FileVersion; }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gpsFile"></param>
        public GPSFile(Core.FileTypes.IFile gpsFile)
        {
            _gpsFile = gpsFile;
        }

    }
}
