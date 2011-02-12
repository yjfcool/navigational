using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Navigational.UIUtilities;

namespace Navigational.WindowsForms.NavigationalControls.FileManager
{
    public partial class FileManager : UserControl
    {
        Core.FileManager.OpenGPSFilesManager _openFileManager = new Core.FileManager.OpenGPSFilesManager();
        private readonly List<GPSFileDescription> _gpsFileList = new List<GPSFileDescription>();
        private readonly List<FileManagerItem> _fileManagerItems = new List<FileManagerItem>();
        private FileManagerItem _selectedFileManagerItem = null;

        #region Constructors and Initializers.
        public FileManager()
        {
            InitializeComponent();
            _openFileManager.CurrentFolder = @"C:\Users\Public\Documents\GPX";
            LoadFileList();
            CreateFileManagerItems();
            PopulateItemsLayout();
            InitializeEvents();
            navigationalMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;
        }

        private void InitializeEvents()
        {
            tableLayoutPanelFiles.MouseWheel += new MouseEventHandler(tableLayoutPanelFiles_MouseWheel);
        }

        void tableLayoutPanelFiles_MouseWheel(object sender, MouseEventArgs e)
        {
            vScrollBar1.Value = tableLayoutPanelFiles.VerticalScroll.Value;
        }
        #endregion

        #region Files list.
        private void PopulateItemsLayout()
        {
            tableLayoutPanelFiles.RowCount = _fileManagerItems.Count;
            for (int i = 0; i < _fileManagerItems.Count; i++)
            {
                tableLayoutPanelFiles.Controls.Add(_fileManagerItems[i], 0, i);
            }
        }


        private void CreateFileManagerItems()
        {
            foreach (GPSFileDescription fileDescription in _gpsFileList)
            {
                FileManagerItem item = new FileManagerItem(fileDescription);

                item.ItemClicked += new EventHandler(FileManagerItemClicked);

                item.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                if (_fileManagerItems.Count % 2 == 0)
                {
                    item.BackColor = SystemColors.ControlLight;
                    item.DefaultBackgroundColor = SystemColors.ControlLight;
                }
                else
                {
                    item.BackColor = SystemColors.Control;
                    item.DefaultBackgroundColor = SystemColors.Control;
                }
                _fileManagerItems.Add(item);
            }
        }


        private void FileManagerItemClicked(object sender, EventArgs e)
        {
            if (_selectedFileManagerItem != null)
            {
                _selectedFileManagerItem.BackColor = _selectedFileManagerItem.DefaultBackgroundColor;
                foreach (Core.NavigationElements.ITrack track in _selectedFileManagerItem.File.Tracks)
                {
                    navigationalMapControl1.RemoveTrack(track);
                }
            }

            _selectedFileManagerItem = sender as FileManagerItem;
            _selectedFileManagerItem.BackColor = SystemColors.GradientActiveCaption;

            foreach (Core.NavigationElements.ITrack track in _selectedFileManagerItem.File.Tracks)
            {
                Core.NavigationElements.Services s = new Core.NavigationElements.Services();
                var newTrack =  s.TrackSimplifier(track);

                navigationalMapControl1.DrawTrack(newTrack, true);
            }
        }


        private void LoadFileList()
        {
            var filesList = _openFileManager.GetFileInfoList();
            foreach (FileInfo fileinfo in filesList)
            {
                var file = _openFileManager.GetFile(fileinfo.FullName);
                if (file.IsFileValid)
                {
                    _gpsFileList.Add(new GPSFileDescription(file));
                }
            }
        }
        #endregion

        #region Vertical scroll bar.
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            tableLayoutPanelFiles.VerticalScroll.Value = e.NewValue;
        }

        private void tableLayoutPanelFiles_Layout(object sender, LayoutEventArgs e)
        {
            SetVerticalScrollBarAccordingToFilesPanel();
        }

        private void SetVerticalScrollBarAccordingToFilesPanel()
        {
            vScrollBar1.Maximum = tableLayoutPanelFiles.VerticalScroll.Maximum;
            vScrollBar1.Minimum = tableLayoutPanelFiles.VerticalScroll.Minimum;
            vScrollBar1.LargeChange = tableLayoutPanelFiles.VerticalScroll.LargeChange;
            vScrollBar1.SmallChange = tableLayoutPanelFiles.VerticalScroll.SmallChange;
        }
        #endregion

    }
}
