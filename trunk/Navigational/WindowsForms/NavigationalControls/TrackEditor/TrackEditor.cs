using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GeoFramework;
using Navigational.WindowsForms.NavigationalControls.TrackEditor.Markers;

namespace Navigational.WindowsForms.NavigationalControls.TrackEditor
{
    public partial class TrackEditor : UserControl
    {
        private List<RoutePointMarker> _routeMarkers = new List<RoutePointMarker>();
        private GMapOverlay _routeOverlay = null;
        private int _markerCounter = 0;

        private GMapMarker _selectedMarker = null;

        private Size _editorButtonSize = Size.Empty;
        private bool _markerDragged = false;

        private Core.NavigationElements.ITrack _tempTrack = null;

        private readonly Distance _ghostMarkerThresholdDistance;

        private Dictionary<GMapRoute, List<GhostPoint>> _ghostPointDictionary = new Dictionary<GMapRoute, List<GhostPoint>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public TrackEditor()
        {
            InitializeComponent();
            InitializeEvents();
            LoadValuesFromConfiguration();

            _routeOverlay = new GMapOverlay(navigationalMapControl1, "EditRoutes");
            _routeOverlay.IsVisibile = true;

            #region Initilize ghost marker distance.
            double ghostMarkerDistance;
            bool ok = double.TryParse(Core.Globals.Configuration.GetInstance().TrackEditorConfiguration.GhostMarkerThresholdDistance, out ghostMarkerDistance);
            if (ok)
            {
                _ghostMarkerThresholdDistance = new Distance(ghostMarkerDistance, DistanceUnit.Meters);
            }
            else
            {
                _ghostMarkerThresholdDistance = new Distance(100d, DistanceUnit.Meters);
            }
            #endregion

            TestRoute();
        }

        private void TestRoute()
        {
            Core.FileManager.OpenGPSFilesManager fileManager = new Core.FileManager.OpenGPSFilesManager();
            var file = fileManager.GetFile(@"C:\Users\Public\Documents\GPX\538211.gpx");

            _routeOverlay.IsVisibile = true;
            navigationalMapControl1.Overlays.Add(_routeOverlay);

            if (file.Tracks.Count > 0)
            {
                Core.NavigationElements.Services service = new Core.NavigationElements.Services();
                _tempTrack = service.TrackSimplifier(file.Tracks[0]);

                navigationalMapControl1.DrawTrack(_tempTrack, true);

                List<GMapRoute> routes = navigationalMapControl1.GetRoute(_tempTrack);

                foreach (GMapRoute route in routes)
                {
                    for (int i = 0; i < route.Points.Count; i++)
                    {
                        var marker = new RoutePointMarker(route.Points[i], i, route);
                        marker.IsVisible = true;
                        _routeMarkers.Add(marker);
                        _routeOverlay.Markers.Add(marker);
                    }
                }
            }
        }

        #region Values from configuration.
        private void LoadValuesFromConfiguration()
        {
            LoadEditorButtonsSizeFromConfiguration();
        }

        private void LoadEditorButtonsSizeFromConfiguration()
        {
            string[] sizeString = Core.Globals.Configuration.GetInstance().TrackEditorConfiguration.EditorButtonsSize.Split(',');
            if (sizeString.Length == 2)
            {
                int width, height;
                bool ok = int.TryParse(sizeString[0], out width);
                ok &= int.TryParse(sizeString[1], out height);

                if (ok)
                {
                    Size size = new Size(width, height);
                    _editorButtonSize = size;
                }
            }
        }
        #endregion

        #region Events
        private void InitializeEvents()
        {
            navigationalMapControl1.Paint += new PaintEventHandler(navigationalMapControl1_Paint);
            navigationalMapControl1.MouseDown += new MouseEventHandler(navigationalMapControl1_MouseDown);
            navigationalMapControl1.MouseUp += new MouseEventHandler(navigationalMapControl1_MouseUp);
            navigationalMapControl1.MouseMove += new MouseEventHandler(navigationalMapControl1_MouseMove);
        }

        void navigationalMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selectedMarker != null)
            {
                _markerDragged = true;
            }
            else
            {
                _markerDragged = false;
                MouseClicked(e.X, e.Y, navigationalMapControl1.FromLocalToLatLng(e.X, e.Y));
            }
            _selectedMarker = null;
        }

        void navigationalMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectedMarker != null)
            {
                _selectedMarker.Position = navigationalMapControl1.FromLocalToLatLng(e.X, e.Y);

                if (_selectedMarker is RoutePointMarker)
                {
                    RoutePointMarker marker = (RoutePointMarker)_selectedMarker;

                    var point = marker.Route.Points[marker.RoutePointIndex];

                    point.Lat = _selectedMarker.Position.Lat;
                    point.Lng = _selectedMarker.Position.Lng;

                    marker.Route.Points[marker.RoutePointIndex] = point;

                    CalculateGhostMarker(marker.Route, marker.RoutePointIndex);

                    marker.Route.Overlay.ForceUpdate();

                }
            }
        }

        void navigationalMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (GMapRoute route in _ghostPointDictionary.Keys)
            {
                foreach (GhostPoint ghostPoint in _ghostPointDictionary[route])
                {
                    if (ghostPoint.Marker.IsMouseOver)
                    {

                        AddRouteMarkerFromGhostMarker(ghostPoint, route);

                        return;
                    }
                }
            }

            foreach (GMapMarker marker in _routeMarkers)
            {
                if (marker.IsMouseOver)
                {
                    _selectedMarker = marker;
                    return;
                }
            }
            _selectedMarker = null;
        }

        

        void navigationalMapControl1_Paint(object sender, PaintEventArgs e)
        {
            PerformUnderMapControlsPaint(e.Graphics, e.ClipRectangle);
        }
        #endregion

        #region Ghost Markers.
        private void CalculateGhostMarker(GMapRoute gMapRoute, int routePoint)
        {
            if (routePoint > 0 && routePoint < gMapRoute.Points.Count - 1)
            {
                var previousPoint = gMapRoute.Points[routePoint - 1];
                var nextPoint = gMapRoute.Points[routePoint + 1];
                var currentPoint = gMapRoute.Points[routePoint];

                GeoFramework.Position previousPosition = new Position(previousPoint.Lat.ToString(), previousPoint.Lng.ToString());
                GeoFramework.Position nextPosition = new Position(nextPoint.Lat.ToString(), nextPoint.Lng.ToString());
                GeoFramework.Position currentPosition = new Position(currentPoint.Lat.ToString(), currentPoint.Lng.ToString());

                CalculateGhostPoint(routePoint, previousPosition, currentPosition, gMapRoute);
                CalculateGhostPoint(routePoint + 1, nextPosition, currentPosition, gMapRoute);
            }
        }

        private void CalculateGhostPoint(int index, Position otherPosition, Position currentPosition, GMapRoute route)
        {
            double x, y;
            var point = new GMap.NET.PointLatLng();

            if (currentPosition.DistanceTo(otherPosition) > _ghostMarkerThresholdDistance)
            {
                x = (currentPosition.Latitude.DecimalDegrees + otherPosition.Latitude.DecimalDegrees) / 2;
                y = (currentPosition.Longitude.DecimalDegrees + otherPosition.Longitude.DecimalDegrees) / 2;

                point.Lat = x;
                point.Lng = y;

                int ghostPointIndex = IsGhostPointExist(index, route);
                if (ghostPointIndex == -1)
                {
                    point = CreateGhostPoint(index, point, route);
                }
                else
                {
                    _ghostPointDictionary[route][ghostPointIndex].Marker.Position = point;
                }
            }
            else
            {
                RemoveGhostPointAt(index, route);
            }
        }

        private GMap.NET.PointLatLng CreateGhostPoint(int index, GMap.NET.PointLatLng point, GMapRoute route)
        {
            GhostMarker g = new GhostMarker(point);
            _routeOverlay.Markers.Add(g);

            GhostPoint ghostPoint = new GhostPoint(index, g);
            if (_ghostPointDictionary.ContainsKey(route) == false)
            {
                _ghostPointDictionary.Add(route, new List<GhostPoint>());
            }
            _ghostPointDictionary[route].Add(ghostPoint);
            return point;
        }

        private bool IsGhostPointExistForIndex(int index, GMapRoute route)
        {
            if (_ghostPointDictionary.ContainsKey(route))
            {
                for (int i = 0; i < _ghostPointDictionary[route].Count; i++)
                {
                    if (_ghostPointDictionary[route][i].IndexPoint == index)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private GhostPoint GetGhostPointForIndex(int index, GMapRoute route)
        {
            if (_ghostPointDictionary.ContainsKey(route))
            {
                for (int i = 0; i < _ghostPointDictionary[route].Count; i++)
                {
                    if (_ghostPointDictionary[route][i].IndexPoint == index)
                    {
                        return _ghostPointDictionary[route][i];
                    }
                }
            }

            throw new ApplicationException("No ghost point exist for specific index.");
        }

        private void RemoveGhostPointAt(int index, GMapRoute route)
        {
            int ghostPointIndex = IsGhostPointExist(index, route);
            if (ghostPointIndex > -1)
            {
                _routeOverlay.Markers.Remove(_ghostPointDictionary[route][ghostPointIndex].Marker);
                _ghostPointDictionary[route].RemoveAt(ghostPointIndex);
            }
        }

        /// <summary>
        /// Checks if a ghost point exist between a specific index in the route points.
        /// </summary>
        /// <param name="p">index of a point on the route.</param>
        /// <returns>index of the ghostpoint from the ghost points collection.</returns>
        private int IsGhostPointExist(int p, GMapRoute route)
        {
            if (_ghostPointDictionary.ContainsKey(route))
            {
                for (int i = 0; i < _ghostPointDictionary[route].Count; i++)
                {
                    if (_ghostPointDictionary[route][i].IndexPoint == p)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Increases ghost point index.
        /// <remarks>This is helpful when adding a true marker from a ghost marker.</remarks>
        /// </summary>
        /// <param name="fromIndex"></param>
        private void IncreaseGhostPointsPosition(int fromIndex, GMapRoute route)
        {
            if (fromIndex < _ghostPointDictionary[route].Count)
            {
                for (int i = fromIndex; i < _ghostPointDictionary[route].Count; i++)
                {
                    _ghostPointDictionary[route][i]++;
                }
            }
        }

        private void AddRouteMarkerFromGhostMarker(GhostPoint ghostPoint, GMapRoute route)
        {
            var routeMarker = new RoutePointMarker(ghostPoint.Marker.Position,
                ghostPoint.IndexPoint,
                route);

            //_routeMarkers.Remove(ghostPoint.Marker);
            _routeMarkers.Insert(ghostPoint.IndexPoint, routeMarker);

            _routeOverlay.Markers.Add(routeMarker);

            route.Points.Insert(ghostPoint.IndexPoint, routeMarker.Position);

            //RemoveGhostPointAt(ghostPoint.IndexPoint , route);

            for (int i = ghostPoint.IndexPoint + 1; i < _routeMarkers.Count; i++)
            {

                if (IsGhostPointExistForIndex(_routeMarkers[i].RoutePointIndex, route))
                {
                    GetGhostPointForIndex(_routeMarkers[i].RoutePointIndex, route).SetIndexPoint(i);
                }

                ((RoutePointMarker)_routeMarkers[i]).SetRouteIndex(i);

            }


            //for (int i = ghostPoint.IndexPoint + 1; i < _ghostPointDictionary[route].Count; i++)
            //{
            //    _ghostPointDictionary[route][i].SetIndexPoint(i + 1);
            //}

            _selectedMarker = routeMarker;

            //CreateGhostPoint(ghostPoint.IndexPoint-1, routeMarker.Position, route);
        }
        #endregion

        public void PerformOnTopPaint(Graphics graphics, Rectangle clipRectangle)
        {

        }

        public void PerformUnderMapControlsPaint(Graphics graphics, Rectangle clipRectangle)
        {
            DrawEditControls(graphics);
        }

        private void DrawEditControls(Graphics graphics)
        {
            Rectangle toolBoxRectangle = new Rectangle(15, 100, _editorButtonSize.Width + 4, 250);

            System.Drawing.Drawing2D.LinearGradientBrush boxBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                toolBoxRectangle,
                SystemColors.ControlLightLight,
                SystemColors.ControlDarkDark,
                45.0f);

            graphics.FillRectangle(boxBrush, toolBoxRectangle);
            Pen pen = new Pen(Brushes.Black, 2.0f);
            graphics.DrawRectangle(pen, toolBoxRectangle);
        }


        public void MouseClicked(int x, int y, GMap.NET.PointLatLng pointLatLng)
        {
            #region Legacy.
            //if (navigationalMapControl1.IsDragging == false &&
            //    navigationalMapControl1.ClickedAlreadyPerformed == false &&
            //    _markerDragged == false)
            //{
            //    EditMarker marker = new EditMarker(pointLatLng, _markerCounter);
            //    _markerCounter++;
            //    marker.IsVisible = true;
            //    _routeOverlay.Markers.Add(marker);
            //    _routeOverlay.IsVisibile = true;
            //    _routeMarkers.Add(marker);
            //    if (navigationalMapControl1.Overlays.Contains(_routeOverlay) == false)
            //    {
            //        navigationalMapControl1.Overlays.Add(_routeOverlay);
            //    }
            //} 
            #endregion
        }
    }



    #region Legacy.
    //class DotMarker : GMapMarker
    //{
    //    Core.NavigationElements.IWayPoint _wayPoint = null;

    //    public Core.NavigationElements.IWayPoint WayPoint
    //    {
    //        get { return _wayPoint; }
    //        set { _wayPoint = WayPoint; }
    //    }

    //    public DotMarker(GMap.NET.PointLatLng p, Core.NavigationElements.IWayPoint waypoint)
    //        : base(p)
    //    {
    //        _wayPoint = waypoint;
    //        base.Size = new Size(8, 8);
    //        Offset = new Point(-4, 4);
    //    }


    //    public override void OnRender(Graphics g)
    //    {
    //        base.OnRender(g);
    //        g.FillEllipse(Brushes.Red, LocalPosition.X + 1, LocalPosition.Y + 1, 6, 6);
    //        g.DrawEllipse(Pens.Black, LocalPosition.X, LocalPosition.Y, 8, 8);
    //    }


    //}



    //class EditMarker : GMapMarker
    //{
    //    private int _number = 0;

    //    private static float _defaultMarkerSize = 0.0f;
    //    private static float _defaultMarkerPenWidth = 0.0f;
    //    private static Color _defaultMarkerColor = Color.Empty;

    //    private Color _markerColor = Color.Empty;
    //    private float _markerSize = 0.0f;
    //    private float _markerPenWidth = 0.0f;

    //    private static bool _configurationLoaded = false;

    //    #region Properties.
    //    /// <summary>
    //    /// Gets or Sets marker color.
    //    /// </summary>
    //    public Color MarkerColor
    //    {
    //        get
    //        {
    //            if (_markerColor == Color.Empty)
    //            {
    //                return _defaultMarkerColor;
    //            }
    //            else
    //            {
    //                return _markerColor;
    //            }
    //        }
    //        set
    //        {
    //            _markerColor = value;
    //        }
    //    }
    //    /// <summary>
    //    /// Gets or Sets marker shape size.
    //    /// </summary>
    //    public float MarkerSize
    //    {
    //        get
    //        {
    //            if (_markerSize == 0.0f)
    //            {
    //                return _defaultMarkerSize;
    //            }
    //            else
    //            {
    //                return _markerSize;
    //            }
    //        }
    //        set
    //        {
    //            _markerSize = value;
    //        }
    //    }
    //    /// <summary>
    //    /// Get or Sets marker pen's width.
    //    /// </summary>
    //    public float MarkerPenWidth
    //    {
    //        get
    //        {
    //            if (_markerPenWidth == 0.0f)
    //            {
    //                return _defaultMarkerPenWidth;
    //            }
    //            else
    //            {
    //                return _markerPenWidth;
    //            }
    //        }
    //        set
    //        {
    //            _markerPenWidth = value;
    //        }
    //    }
    //    #endregion

    //    #region Constructors and Initializers.
    //    /// <summary>
    //    /// Constructor.
    //    /// </summary>
    //    /// <param name="p"></param>
    //    /// <param name="number"></param>
    //    public EditMarker(GMap.NET.PointLatLng p, int number)
    //        : base(p)
    //    {
    //        ToolTipText = "Marker no." + number.ToString();
    //        _number = number;
    //        base.Size = new Size(30, 30);
    //        Offset = new Point(((-1) * Size.Width / 2), ((-1) * Size.Height / 2));
    //        if (_configurationLoaded == false)
    //        {
    //            LoadValuesFromConfiguration();
    //        }


    //    }

    //    #region Values from configuration.
    //    private void LoadValuesFromConfiguration()
    //    {
    //        _configurationLoaded = true;

    //        PenWidthFromConfiguration();
    //        MarkerSizeFromConfiguration();
    //        MarkerColorFromConfiguration();
    //    }

    //    private static void MarkerColorFromConfiguration()
    //    {
    //        string markerColorString = Core.Globals.Configuration.GetInstance().TrackEditorConfiguration.MarkerColor;
    //        int argb;
    //        if (int.TryParse(markerColorString, out argb))
    //        {
    //            _defaultMarkerColor = Color.FromArgb(argb);
    //        }
    //    }

    //    private static void MarkerSizeFromConfiguration()
    //    {
    //        string markerSizeString = Core.Globals.Configuration.GetInstance().TrackEditorConfiguration.MarkerSize;
    //        float.TryParse(markerSizeString, out _defaultMarkerSize);
    //    }

    //    private static void PenWidthFromConfiguration()
    //    {
    //        string penWidthString = Core.Globals.Configuration.GetInstance().TrackEditorConfiguration.MarkerPenWidth;
    //        float.TryParse(penWidthString, out _defaultMarkerPenWidth);
    //    }
    //    #endregion

    //    #endregion

    //    public override void OnRender(Graphics g)
    //    {
    //        base.OnRender(g);

    //        g.DrawRectangle(Pens.Black, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);

    //        float horizontalCenter = (Size.Width / 2) - MarkerSize;
    //        float verticalCenter = (Size.Height / 2) - MarkerSize;

    //        PointF centerPoint = new PointF(LocalPosition.X + horizontalCenter, LocalPosition.Y + verticalCenter);

    //        g.DrawEllipse(new Pen(new SolidBrush(MarkerColor), MarkerPenWidth),
    //            centerPoint.X,
    //            centerPoint.Y,
    //            MarkerSize * 2,
    //            MarkerSize * 2);

    //        g.DrawString(_number.ToString(), SystemFonts.DefaultFont,
    //            Brushes.Black,
    //            new PointF(LocalPosition.X - 5, LocalPosition.Y + 5));
    //    }
    //} 
    #endregion
}
