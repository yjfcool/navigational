using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes
{
    class InvalidFile : IFile
    {
        #region Implementation of IFile

        /// <summary>
        /// Gets file version.
        /// </summary>
        public string FileVersion
        {
            get { return "0.0"; }
        }

        /// <summary>
        /// Gets file type.
        /// </summary>
        public string FileType
        {
            get { return "InvalidFile"; }
        }

        /// <summary>
        /// Gets the gps filename.
        /// </summary>
        public string FileName
        {
            get { throw new Exceptions.FileIsNotValidException(); }
        }

        /// <summary>
        /// Gets if parsing the file was completed succesfully.
        /// </summary>
        public bool IsFileValid
        {
            get { return false; }
        }

        /// <summary>
        /// Gets file's details.
        /// </summary>
        public IFileDetails FileDetails
        {
            get { throw new Exceptions.FileIsNotValidException(); }
        }

        /// <summary>
        /// Gets a list of the gps file's tracks.
        /// </summary>
        public List<ITrack> Tracks
        {
            get { throw new Exceptions.FileIsNotValidException(); }
        }

        /// <summary>
        /// Gets a list of the gps file's routes.
        /// </summary>
        public List<IRoute> Routes
        {
            get { throw new Exceptions.FileIsNotValidException(); }
        }

        /// <summary>
        /// Gets a list of the gps file's waypoints.
        /// </summary>
        public List<IWayPoint> WayPoints
        {
            get { throw new Exceptions.FileIsNotValidException(); }
        }

        #endregion
    }
}
