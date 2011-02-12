using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using Core.NavigationElements;
using System.ComponentModel.Design;
using System.Drawing;

namespace Navigational.WindowsForms.NavigationalMap
{
    class NavigationalMapControl : GMap.NET.WindowsForms.GMapControl, IDisposable
    {
        private GMap.NET.WindowsForms.GMapOverlay _trackOverlay;
        private Dictionary<string, List<GMap.NET.WindowsForms.GMapRoute>> _mapRoutes = new Dictionary<string, List<GMapRoute>>();
        
        private Rectangle _rectangleZoomInButton = Rectangle.Empty;
        private Rectangle _rectangleZoomOutButton = Rectangle.Empty;
        private Image _imageZoomInButton = null;
        private Image _imageZoomOutButton = null;
        private Size _zoomButtonsSize = new Size(40,40);

        private Point _mouseDownPoint = Point.Empty;

        private GMap.NET.PointLatLng _startupPoint = GMap.NET.PointLatLng.Empty;
        private bool _clickAlreadyPerformed = false;
        Region _locationRegion = new Region(new Rectangle(10, 60, 300, 50));

        #region Properties
        /// <summary>
        /// Gets if a mouse click event already performed its operation.
        /// </summary>
        public bool ClickedAlreadyPerformed
        {
            get { return _clickAlreadyPerformed; }
        }
        #endregion


        #region Constructors and Initializers.
        public NavigationalMapControl()
            : base()
        {
            InitializeMapBasics();

            InitializeEvents();

            InitializeZoomButtons();

            SetMapTypeFromConfiguration();
            SetMapLanguageFromConfiguration();

            _trackOverlay = new GMap.NET.WindowsForms.GMapOverlay(this, "tracks");
            Overlays.Add(_trackOverlay);

            base.RasterBaseFolder = @"C:\AmudAnan\Tiles";
        }

        private void InitializeMapBasics()
        {
            Zoom = 7;
            MinZoom = 7;
            MaxZoom = 17;
            base.DragButton = System.Windows.Forms.MouseButtons.Left;
            ParseStartupLocation();
            Position = _startupPoint;
        }

        private void ParseStartupLocation()
        {
            if (UIUtilities.Services.IsDesignMode() == false)
            {
                string[] stringPoint = Core.Globals.Globals.MapStartupLocation.Split(',');

                if (stringPoint.Length == 2)
                {
                    double lat, lon;

                    bool ok = double.TryParse(stringPoint[0], out lat);
                    ok &= double.TryParse(stringPoint[1], out lon);

                    if (ok)
                    {
                        _startupPoint.Lat = lat;
                        _startupPoint.Lng = lon;
                    }
                }
            }
        }

        private void InitializeEvents()
        {
            //this.MouseDown += new System.Windows.Forms.MouseEventHandler(NavigationalMapControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(NavigationalMapControl_MouseMove);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(NavigationalMapControl_MouseClick);
        }

        void NavigationalMapControl_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (base.IsDragging == false)
            {
                bool performed = PerformMouseUpHitTests(e.Location);

                if (performed == false)
                {
                    _clickAlreadyPerformed = false;
                }
                else
                {
                    _clickAlreadyPerformed = true;
                }
            }   
        }


        void NavigationalMapControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Invalidate(_locationRegion);
        }


        private bool PerformMouseUpHitTests(Point point)
        {
            return HitTestZoomButtons(point);
        }

        private bool HitTestZoomButtons(Point point)
        {
            Rectangle hitRectangle = new Rectangle(point.X,point.Y,1,1);
            
            if (_rectangleZoomInButton.IntersectsWith(hitRectangle) == true)
            {
                PerformZoomIn();
                return true;
            }
            if (_rectangleZoomOutButton.IntersectsWith(hitRectangle) == true)
            {
                PerformZoomOut();
                return true;
            }

            return false;
        }

        private void PerformZoomOut()
        {
            base.Zoom--;
        }

        private void PerformZoomIn()
        {
            base.Zoom++;
        }
      
        private void InitializeZoomButtons()
        {
            _imageZoomInButton = Properties.Resources.plus;
            _imageZoomOutButton = Properties.Resources.minus;

            SetZoomButtonsRectangles();
        }

        private void SetZoomButtonsRectangles()
        {
            _rectangleZoomOutButton = new Rectangle(
                this.Location.X + 10,
                this.Location.Y + 10,
                _zoomButtonsSize.Width,
                _zoomButtonsSize.Height);

            _rectangleZoomInButton = new Rectangle(
                this.Location.X - 10 - _zoomButtonsSize.Width + this.Width,
                this.Location.Y + 10,
                _zoomButtonsSize.Width,
                _zoomButtonsSize.Height);    
        }

        private void SetMapLanguageFromConfiguration()
        {
            if (UIUtilities.Services.IsDesignMode() == false)
            {
                if (Enum.IsDefined(typeof(GMap.NET.LanguageType), Core.Globals.Globals.MapLanguage))
                {
                    Manager.Language = (GMap.NET.LanguageType)Enum.Parse(typeof(GMap.NET.LanguageType), Core.Globals.Globals.MapLanguage);
                }
            }
        }

        private void SetMapTypeFromConfiguration()
        {
            if (UIUtilities.Services.IsDesignMode() == false)
            {
                if (Enum.IsDefined(typeof(GMap.NET.MapType), Core.Globals.Globals.MapType))
                {
                    MapType = (GMap.NET.MapType)Enum.Parse(typeof(GMap.NET.MapType), Core.Globals.Globals.MapType);
                }
            }
        }
        #endregion

        #region Overrides

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawZoomButtons(e.Graphics);
            DrawLocationBox(e.Graphics);

        }

        private void DrawLocationBox(Graphics graphics)
        {
            Point mousePoint = PointToClient(new Point(MousePosition.X, MousePosition.Y));
            graphics.DrawString(String.Format("Mouse: {0},{1}", mousePoint.X, mousePoint.Y), SystemFonts.DefaultFont, Brushes.Black, new PointF(10,60));
            
            graphics.DrawString(String.Format("Lat/Lon: {0}", FromLocalToLatLng(mousePoint.X,mousePoint.Y)),
                SystemFonts.DefaultFont,
                Brushes.Black,
                new PointF(10, 80));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SetZoomButtonsRectangles();
        }
        #endregion.

        #region Draw Tracks.

        #region Public Methods.
        public List<GMapRoute> GetRoute(Core.NavigationElements.ITrack track)
        {
            if (_mapRoutes.ContainsKey(track.UniqueID))
            {
                return _mapRoutes[track.UniqueID]; 
            }
            return new List<GMapRoute>(0);
        }
        /// <summary>
        /// Refreshes a secific track.
        /// </summary>
        /// <param name="track"></param>
        public void RefreshTrack(Core.NavigationElements.ITrack track)
        {
            // TODO: Need to nice refrsh tracks.
            if (_mapRoutes.ContainsKey(track.UniqueID))
            {
                foreach (GMapRoute route in _mapRoutes[track.UniqueID])
                {
                    _trackOverlay.Routes.Remove(route);
                }

                foreach (Core.NavigationElements.ITrackSegment trackSegment in track.TrackSegments)
                {
                    GMapRoute route = new GMapRoute(ConvertTrackSegment(trackSegment), "segment");
                    _trackOverlay.Routes.Add(route);
                }

                base.Refresh();
            }
        }
        /// <summary>
        /// Draws track on map.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="zoomAndCenter">Whether to zoom and center on the current track or not.</param>
        public void DrawTrack(Core.NavigationElements.ITrack track, bool zoomAndCenter)
        {
            DrawTrack(track);
            if (zoomAndCenter)
            {
                ZoomAndCenterTrack(track);
            }
        }

        public void RemoveTrack(Core.NavigationElements.ITrack track)
        {
            if (_mapRoutes.ContainsKey(track.UniqueID))
            {
                foreach (GMap.NET.WindowsForms.GMapRoute route in _mapRoutes[track.UniqueID])
                {
                    _trackOverlay.Routes.Remove(route);
                }
            }
        }
        #endregion

        #region Private Methods.
        private void DrawTrack(Core.NavigationElements.ITrack track)
        {
            if (_mapRoutes.ContainsKey(track.UniqueID) == false)
            {
                _mapRoutes.Add(track.UniqueID, null);
            }


            List<GMap.NET.WindowsForms.GMapRoute> routes = new List<GMapRoute>();
            foreach (ITrackSegment segment in track.TrackSegments)
            {
                List<GMap.NET.PointLatLng> points = ConvertTrackSegment(segment);
                var route = new GMap.NET.WindowsForms.GMapRoute(points, "segment");
                routes.Add(route);
                _trackOverlay.Routes.Add(route);
            }

            _mapRoutes[track.UniqueID] = routes;
            _trackOverlay.IsVisibile = true;
        }

        private void ZoomAndCenterTrack(Core.NavigationElements.ITrack track)
        {
            if (_mapRoutes[track.UniqueID].Count > 0)
            {
                this.ZoomAndCenterRoute(_mapRoutes[track.UniqueID][0]);
            }
        }

        private List<GMap.NET.PointLatLng> ConvertTrackSegment(ITrackSegment segment)
        {
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>(segment.SegmentWaypoints.Count);

            for (int i = 0; i < segment.SegmentWaypoints.Count; i++)
            {
                GMap.NET.PointLatLng point = new GMap.NET.PointLatLng();
                point.Lat = (double)segment.SegmentWaypoints[i].Latitude;
                point.Lng = (double)segment.SegmentWaypoints[i].Longitude;

                points.Add(point);
            }
            return points;
        }
        #endregion

        #endregion

        #region Map Controls.
        private void DrawZoomButtons(Graphics g)
        {
            DrawZoomInButton(g);
            DrawZoomOutButton(g);
        }

        private void DrawZoomInButton(Graphics g)
        {
            g.DrawImage(_imageZoomInButton, _rectangleZoomInButton);
        }

        private void DrawZoomOutButton(Graphics g)
        {
            g.DrawImage(_imageZoomOutButton, _rectangleZoomOutButton);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            _locationRegion.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NavigationalMapControl
            // 
            this.MinZoom = 3;
            this.Name = "NavigationalMapControl";
            this.ResumeLayout(false);

        }
    }
}
