using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Timers;
using System.IO;

namespace Navigational.WPF.NavigationalControls.FileManager
{
    /// <summary>
    /// Interaction logic for FileManager.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for FileManager.xaml
    /// </summary>
    public partial class FileManager : UserControl
    {

        private Core.FileManager.OpenGPSFilesManager _openFileManager = null;
        private readonly List<GPSFile> _gpsFile = new List<GPSFile>();

        #region Scrolling.

        #region Scrolling Members.

        private readonly DoubleAnimation _scrollAnimation = new DoubleAnimation();
        private bool _animationStopped = true;
        private Point _mouseDragStartPoint;
        private DateTime _mouseDownTime;
        private Point _scrollStartOffset;
        private const double DECELERATION = 980;
        private const double SPEED_RATIO = .5;
        private const double MAX_VELOCITY = 2500;
        private const double MIN_DISTANCE = 0;
        private const double TIME_THRESHOLD = .4;

        private readonly Timer _timer = new Timer();
        #endregion

        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.Register("ScrollOffset", typeof(double), typeof(FileManager), new UIPropertyMetadata(FileManager.ScrollOffsetValueChanged));
        public double ScrollOffset
        {
            get { return (double)GetValue(ScrollOffsetProperty); }
            set { SetValue(ScrollOffsetProperty, value); }
        }

        private static void ScrollOffsetValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileManager myClass = (FileManager)d;
            myClass.FileListScrollViewer.ScrollToVerticalOffset((double)e.NewValue);
        }



        private void Scroll(double startY, double endY, DateTime startTime, DateTime endTime)
        {
            double timeScrolled = endTime.Subtract(startTime).TotalSeconds;

            //if scrolling slowly, don't scroll with force
            if (timeScrolled < TIME_THRESHOLD)
            {
                double distanceScrolled = Math.Max(Math.Abs(endY - startY), MIN_DISTANCE);

                double velocity = distanceScrolled / timeScrolled;
                velocity = Math.Min(MAX_VELOCITY, velocity);
                int direction = 1;

                if (endY > startY)
                {
                    direction = -1;
                }

                double timeToScroll = (velocity / DECELERATION) * SPEED_RATIO;

                double distanceToScroll = ((velocity * velocity) / (2 * DECELERATION)) * SPEED_RATIO;

                if (Math.Abs(distanceToScroll) < 1)
                {
                    timeToScroll = 0;
                    distanceToScroll = 0;
                    if (_animationStopped)
                    {
                        CellClicked();
                    }
                }

                _scrollAnimation.From = FileListScrollViewer.VerticalOffset;
                _scrollAnimation.To = FileListScrollViewer.VerticalOffset + distanceToScroll * direction;
                _scrollAnimation.DecelerationRatio = .9;
                _scrollAnimation.SpeedRatio = SPEED_RATIO;
                _scrollAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, Convert.ToInt32(timeToScroll), 0));

                if (_scrollAnimation.Duration.TimeSpan.TotalMilliseconds > 0)
                {
                    _animationStopped = false;
                    _timer.Interval = _scrollAnimation.Duration.TimeSpan.TotalMilliseconds; // timeToScroll * 1000;
                    _timer.Start();
                }

                BeginAnimation(FileManager.ScrollOffsetProperty, _scrollAnimation);
            }
        }

        private void CellClicked()
        {
            ListViewItem item = GetItemAt(FileListScrollViewer, _mouseDragStartPoint);
            if (item != null)
            {
                DeselectAll(FilesListView);
                string selectedText;
                if (item.Content is GPSFile)
                {
                    item.IsSelected = true;
                    selectedText = String.Format("Item: {0}", ((GPSFile)item.Content).FileName);
                }
                else
                {
                    selectedText = String.Format("Item: {0}", item.ToString());
                }
                selectedTextBlcok.Text = selectedText;
            }
        }

        private static void DeselectAll(ListView listView)
        {
            foreach (object item in listView.Items)
            {
                var listViewItem = listView.ItemContainerGenerator.ContainerFromItem(item);
                ((ListViewItem)listViewItem).IsSelected = false;
            }
        }

        //public static ListViewItem GetItemAt(ListView listView, Point clientRelativePosition)
        //{
        //    var hitTestResult = VisualTreeHelper.HitTest(listView, clientRelativePosition);
        //    var selectedItem = hitTestResult.VisualHit;
        //    while (selectedItem != null)
        //    {
        //        if (selectedItem is ListViewItem)
        //        {
        //            break;
        //        }
        //        selectedItem = VisualTreeHelper.GetParent(selectedItem);
        //    }
        //    return selectedItem != null ? ((ListViewItem)selectedItem) : null;
        //}

        public static ListViewItem GetItemAt(ScrollViewer scrollViewer, Point clientRelativePosition)
        {
            var hitTestResult = VisualTreeHelper.HitTest(scrollViewer, clientRelativePosition);
            var selectedItem = hitTestResult.VisualHit;
            while (selectedItem != null)
            {
                if (selectedItem is ListViewItem)
                {
                    break;
                }
                selectedItem = VisualTreeHelper.GetParent(selectedItem);
            }
            return selectedItem != null ? ((ListViewItem)selectedItem) : null;
        }


        #region Mouse Overrides
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            _mouseDragStartPoint = e.GetPosition(this);
            _mouseDownTime = DateTime.Now;
            _scrollStartOffset.X = FileListScrollViewer.HorizontalOffset;
            _scrollStartOffset.Y = FileListScrollViewer.VerticalOffset;

            // Update the cursor if scrolling is possible 
            this.Cursor = (FileListScrollViewer.ExtentWidth > FileListScrollViewer.ViewportWidth) ||
                (FileListScrollViewer.ExtentHeight > FileListScrollViewer.ViewportHeight) ?
                Cursors.ScrollAll : Cursors.Arrow;

            this.CaptureMouse();
            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                // Get the new mouse position. 
                Point mouseDragCurrentPoint = e.GetPosition(this);

                // Determine the new amount to scroll. 
                Point delta = new Point(
                    (mouseDragCurrentPoint.X > this._mouseDragStartPoint.X) ?
                    -(mouseDragCurrentPoint.X - this._mouseDragStartPoint.X) :
                    (this._mouseDragStartPoint.X - mouseDragCurrentPoint.X),
                    (mouseDragCurrentPoint.Y > this._mouseDragStartPoint.Y) ?
                    -(mouseDragCurrentPoint.Y - this._mouseDragStartPoint.Y) :
                    (this._mouseDragStartPoint.Y - mouseDragCurrentPoint.Y));



                // Scroll to the new position. 
                FileListScrollViewer.ScrollToHorizontalOffset(this._scrollStartOffset.X + delta.X);
                FileListScrollViewer.ScrollToVerticalOffset(this._scrollStartOffset.Y + delta.Y);
            }
            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.Cursor = Cursors.Arrow;
                this.ReleaseMouseCapture();
            }

            Scroll(_mouseDragStartPoint.Y, e.GetPosition(this).Y, _mouseDownTime, DateTime.Now);
            base.OnPreviewMouseUp(e);
        }

        #endregion
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public FileManager()
        {
            InitializeComponent();
            InitializeEvents();
            InitializeFileManager();
            LoadFileList();
        }

        private void InitializeEvents()
        {
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);

        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _animationStopped = true;
        }

        private void InitializeFileManager()
        {
            _openFileManager = new Core.FileManager.OpenGPSFilesManager();
            _openFileManager.CurrentFolder = @"C:\Users\Public\Documents\GPX";
        }

        private void LoadFileList()
        {
            //List<string> filters = new List<string>(2);

            var filesList = _openFileManager.GetFileInfoList();
            foreach (FileInfo fileinfo in filesList)
            {
                var file = _openFileManager.GetFile(fileinfo.FullName);
                if (file.IsFileValid)
                {
                    _gpsFile.Add(new GPSFile(file));
                    _gpsFile.Add(new GPSFile(file));
                    _gpsFile.Add(new GPSFile(file));
                    _gpsFile.Add(new GPSFile(file));
                }
            }
            FilesListView.ItemsSource = null;
            FilesListView.ItemsSource = _gpsFile;

        }
    }
}
