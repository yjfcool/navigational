using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.NavigationElements;

namespace Core.FileTypes
{
    /// <summary>
    /// Information about the GPS file
    /// </summary>
    public interface IFileDetails
    {
        /// <summary>
        /// Gets GPS File's description
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Gets GPS File's time and date.
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Keywords associated with the file. Search engines or databases can use this information to classify the data.
        /// </summary>
        string Keywords { get; }

        /// <summary>
        /// Minimum and maximum coordinates which describe the extent of the coordinates in the file.
        /// </summary>
        IBounds Bounds { get; }
       
    }
}
