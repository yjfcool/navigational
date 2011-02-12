using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core.NavigationElements;

namespace Core.FileTypes.GPX
{
    class GPX11FileDetails : IFileDetails
    {
        private XSD.Ver11.gpxType _gpxType = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="file"></param>
        public GPX11FileDetails(XSD.Ver11.gpxType gpxType)
        {
            _gpxType = gpxType;
        }

        /// <summary>
        /// Gets GPS File's description
        /// </summary>
        public string Description
        {
            get
            {
                if (_gpxType.metadata != null && _gpxType.metadata.desc != null)
                {
                    return _gpxType.metadata.desc;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets GPS File's time and date.
        /// </summary>
        public DateTime Date
        {
            get
            {
                if (_gpxType.metadata != null)
                {
                    return _gpxType.metadata.time;
                }
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Keywords associated with the file. Search engines or databases can use this information to classify the data.
        /// </summary>
        public string Keywords
        {
            get
            {
                return _gpxType.metadata.keywords;
            }
        }

        /// <summary>
        /// Minimum and maximum coordinates which describe the extent of the coordinates in the file.
        /// </summary>
        public IBounds Bounds
        {
            get { return new GPX11Bounds(_gpxType.metadata.bounds); }
        }
    }
}
