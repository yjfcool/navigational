using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.FileTypes.GPX.XSD.Ver10;
using Core.NavigationElements;

namespace Core.FileTypes.GPX.GPX10
{
    class GPX10FileDetails:IFileDetails
    {
        private gpx _gpxType = null;

        public GPX10FileDetails(gpx gpx10)
        {
            _gpxType = gpx10;
        }

        #region Implementation of IFileDetails

        /// <summary>
        /// Gets GPS File's description
        /// </summary>
        public string Description
        {
            get { return _gpxType.desc; }
        }

        /// <summary>
        /// Gets GPS File's time and date.
        /// </summary>
        public DateTime Date
        {
            get { return _gpxType.time; }
        }

        /// <summary>
        /// Keywords associated with the file. Search engines or databases can use this information to classify the data.
        /// </summary>
        public string Keywords
        {
            get { return _gpxType.keywords; }
        }

        /// <summary>
        /// Minimum and maximum coordinates which describe the extent of the coordinates in the file.
        /// </summary>
        public IBounds Bounds
        {
            get { return new GPX10Bounds(_gpxType.bounds); }
        }

        #endregion
    }
}
