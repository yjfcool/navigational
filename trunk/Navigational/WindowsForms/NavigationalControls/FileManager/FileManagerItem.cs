using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Navigational.WindowsForms.NavigationalControls.FileManager
{
    public partial class FileManagerItem : UserControl
    {
        public event EventHandler ItemClicked;

        private Color _defaultBackgroundColor = Color.Empty;
        private Core.FileTypes.IFile _iFile = null;

        /// <summary>
        /// Gets or Sets item background color.
        /// </summary>
        public Color DefaultBackgroundColor
        {
            get
            {
                return _defaultBackgroundColor;
            }
            set
            {
                _defaultBackgroundColor = value;
            }
        }

        /// <summary>
        /// Gets the reference to the actuall file.
        /// </summary>
        public Core.FileTypes.IFile File
        {
            get
            {
                return _iFile;
            }
        }


        #region Constructors and Initializers.
        public FileManagerItem()
        {
            InitializeComponent();
            InitializeEvents();
            base.DoubleBuffered = true;
        }

        public FileManagerItem(UIUtilities.GPSFileDescription gpsFileDescription)
            : this()
        {
            _iFile = gpsFileDescription.File;
            labelFilename.Text = gpsFileDescription.FileName;
            labelGPSFileVersion.Text = gpsFileDescription.FileType;

            labelDateTimeField.Text = gpsFileDescription.Date.ToShortDateString();

            labelDescriptionField.Text = string.Format("Distance: {0}.\n{1}", GetTracksDistance(), gpsFileDescription.Description);

        }

        private string GetTracksDistance()
        {
            GeoFramework.Distance distance = GeoFramework.Distance.Empty;

            for (int i = 0; i < _iFile.Tracks.Count; i++)
            {
                distance += _iFile.Tracks[i].Length;
            }

            if (Core.Globals.Globals.Units == "Miles")
            {
                return distance.ToStatuteMiles().ToString();
            }
            return distance.ToKilometers().ToString();
        }

        private void InitializeEvents()
        {
            foreach (Control control in Controls)
            {
                InitializeClickEvent(control);
            }
        }

        private void InitializeClickEvent(Control parentControl)
        {
            parentControl.Click += new EventHandler(FileManagerItem_Click);
            foreach (Control control in parentControl.Controls)
            {
                InitializeClickEvent(control);
            }
        }
        #endregion

        private void FileManagerItem_Click(object sender, EventArgs e)
        {
            if (ItemClicked != null)
            {
                ItemClicked(this, e);
            }
        }


    }
}
