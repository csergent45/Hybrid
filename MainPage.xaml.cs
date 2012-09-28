using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using ESRI.ArcGIS.Client.Tasks;




namespace hybrid
{
    public partial class MainPage : UserControl
    {
        string _toolMode = "";
        List<Envelope> _extentHistory = new List<Envelope>();
        int _currentExtentIndex = 0;
        bool _newExtent = true;
        Image _previousExtentImage;
        Image _nextExtentImage;

        // Defines the drawing used to draw the zoom box for zoom in and zoom out
        private Draw MyDrawObject;

        // Defines measuring graphics to draw
        private Draw myMeasureObject;

        // Defines the measuring graphics layer
        GraphicsLayer graphicsLayer;

        // Initializes the geometry service for measuring lines and polygons
        GeometryService geometryService = new GeometryService("http://unibeast/ArcGIS/rest/services/Geometry/GeometryServer");
        
        // Generic variable for drawing points, polygons and lines
        private Symbol _activeSymbol = null;
        private string symbology = null;

        public MainPage()
        {
            InitializeComponent();
            MyDrawObject = new Draw(myMap)
            {
                FillSymbol = LayoutRoot.Resources["DefaultFillSymbol"] as ESRI.ArcGIS.Client.Symbols.FillSymbol,
                DrawMode = DrawMode.Rectangle
            };

            MyDrawObject.DrawComplete += myDrawObject_DrawComplete;
            MyDrawObject.IsEnabled = false;
            _toolMode = "";
            _previousExtentImage = btnPrevExtent.Content as Image;
            _nextExtentImage = btnNextExtent.Content as Image;

            // Initializes the graphics layer
            graphicsLayer = myMap.Layers["myGraphicsLayer"] as GraphicsLayer;

            myMeasureObject = new Draw(myMap)
            {
                // Sets the initial drawing of the line and polygon
                FillSymbol = LayoutRoot.Resources["RedFillSymbol"] as ESRI.ArcGIS.Client.Symbols.FillSymbol,
                LineSymbol = LayoutRoot.Resources["RedLineSymbol"] as ESRI.ArcGIS.Client.Symbols.CartographicLineSymbol
            };

            // Runs the measure draw complete method
            myMeasureObject.DrawComplete += myMeasureObject_DrawComplete;
            // Disables the measure drawing once a graphic is drawn - in other words, I don't want to draw multiple graphics
            myMeasureObject.IsEnabled = false;



        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            MyDrawObject.IsEnabled = true;
            // Zoom Map In
            _toolMode = "zoomin";
            // Change the background color of a button on click - http://forums.silverlight.net/p/259771/648943.aspx/1?How+to+change+button+colour+on+mouseclick+
            btnZoomIn.Background = new SolidColorBrush(Colors.White);
            btnZoomOut.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnPan.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnIdentify.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnMeasure.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
                        
        }


        private void myDrawObject_DrawComplete(object sender, DrawEventArgs args)

        {
            // Draws the Zoom In Box
            if (_toolMode == "zoomin")
            {
                myMap.ZoomTo(args.Geometry as ESRI.ArcGIS.Client.Geometry.Envelope);
           
            }
            // Draws the Zoom Out Box
            else if (_toolMode == "zoomout")
            {
                Envelope currentExtent = myMap.Extent;

                Envelope zoomBoxExtent = args.Geometry as Envelope;
                MapPoint zoomBoxCenter = zoomBoxExtent.GetCenter();

                double whRatioCurrent = currentExtent.Width / currentExtent.Height;
                double whRatioZoomBox = zoomBoxExtent.Width / zoomBoxExtent.Height;

                Envelope newEnv = null;

                if (whRatioZoomBox > whRatioCurrent)
                // use width
                {
                    double mapWidthPixels = myMap.Width;
                    double multiplier = currentExtent.Width / zoomBoxExtent.Width;
                    double newWidthMapUnits = currentExtent.Width * multiplier;
                    newEnv = new Envelope(new MapPoint(zoomBoxCenter.X - (newWidthMapUnits / 2), zoomBoxCenter.Y),
                                                   new MapPoint(zoomBoxCenter.X + (newWidthMapUnits / 2), zoomBoxCenter.Y));
                }
                else
                // use height
                {
                    double mapHeightPixels = myMap.Height;
                    double multiplier = currentExtent.Height / zoomBoxExtent.Height;
                    double newHeightMapUnits = currentExtent.Height * multiplier;
                    newEnv = new Envelope(new MapPoint(zoomBoxCenter.X, zoomBoxCenter.Y - (newHeightMapUnits / 2)),
                                                   new MapPoint(zoomBoxCenter.X, zoomBoxCenter.Y + (newHeightMapUnits / 2)));
                }

                if (newEnv != null)
                    myMap.ZoomTo(newEnv);
            }
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            // Zoom Map Out
            MyDrawObject.IsEnabled = true;
            _toolMode = "zoomout";
            btnZoomOut.Background = new SolidColorBrush(Colors.White);
            btnZoomIn.Background = new SolidColorBrush(Color.FromArgb(255,45,132,206));
            btnZoomIn.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnPan.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnIdentify.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnMeasure.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));

        }

        private void btnPan_Click(object sender, RoutedEventArgs e)
        {
            // Pan Map
            MyDrawObject.IsEnabled = false;
            _toolMode = "";
            btnPan.Background = new SolidColorBrush(Colors.White);
            btnZoomIn.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnZoomOut.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnIdentify.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnMeasure.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
        }

        private void btnFullExtent_Click(object sender, RoutedEventArgs e)
        {
            //myMap.ZoomTo(myMap.Layers.GetFullExtent());
            // Zoom to Full Extent
            // This is the extent the full extent button uses
            myMap.ZoomTo(new Envelope(778733.96207758, 1133387.86320644, 849794.792685476, 1193607.08672764));
            btnZoomIn.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnZoomOut.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnPan.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnIdentify.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnMeasure.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));

        }


        private void myMap_ExtentChanged(object sender, ExtentEventArgs e)
        {
            if (e.OldExtent == null)
            {
                _extentHistory.Add(e.NewExtent.Clone());
                return;
            }

            if (_newExtent)
            {
                _currentExtentIndex++;

                if (_extentHistory.Count - _currentExtentIndex > 0)
                    _extentHistory.RemoveRange(_currentExtentIndex, (_extentHistory.Count - _currentExtentIndex));

                if (btnNextExtent.IsHitTestVisible == true)
                {
                    btnNextExtent.Opacity = 0.3;
                    btnNextExtent.IsHitTestVisible = false;
                }

                _extentHistory.Add(e.NewExtent.Clone());

                if (btnPrevExtent.IsHitTestVisible == false)
                {
                    btnPrevExtent.Opacity = 1;
                    btnPrevExtent.IsHitTestVisible = true;
                    
                }
            }
            else
            {
                myMap.IsHitTestVisible = true;
                _newExtent = true;
            }
        }

        private void btnPrevExtent_Click(object sender, RoutedEventArgs e)
        {
            if (_currentExtentIndex != 0)
            {
                _currentExtentIndex--;

                if (_currentExtentIndex == 0)
                {
                    btnPrevExtent.Opacity = 0.3;
                    btnPrevExtent.IsHitTestVisible = false;
                    
                }

                _newExtent = false;

                myMap.IsHitTestVisible = false;
                myMap.ZoomTo(_extentHistory[_currentExtentIndex]);

                if (btnNextExtent.IsHitTestVisible == false)
                {
                    btnNextExtent.Opacity = 1;
                    btnNextExtent.IsHitTestVisible = true;

                }
            }

        }

        private void btnNextExtent_Click(object sender, RoutedEventArgs e)
        {
            if (_currentExtentIndex < _extentHistory.Count - 1)
            {
                _currentExtentIndex++;

                if (_currentExtentIndex == (_extentHistory.Count - 1))
                {
                    btnNextExtent.Opacity = 0.3;
                    btnNextExtent.IsHitTestVisible = false;
                }

                _newExtent = false;

                myMap.IsHitTestVisible = false;
                myMap.ZoomTo(_extentHistory[_currentExtentIndex]);

                if (btnPrevExtent.IsHitTestVisible == false)
                {
                    btnPrevExtent.Opacity = 1;
                    btnPrevExtent.IsHitTestVisible = true;
                }
            }

        }

        private void btnIdentify_Click(object sender, RoutedEventArgs e)
        {
            btnIdentify.Background = new SolidColorBrush(Colors.White);
            btnZoomIn.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnZoomOut.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnPan.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnMeasure.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
        }

        private void btnMeasure_Click(object sender, RoutedEventArgs e)
        {
            btnMeasure.Background = new SolidColorBrush(Colors.White);
            btnZoomIn.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnZoomOut.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnPan.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            btnIdentify.Background = new SolidColorBrush(Color.FromArgb(255, 45, 132, 206));
            measureTools.Visibility = Visibility.Visible;
            
        }

        private void btnCloseTools_Click(object sender, RoutedEventArgs e)
        {
            measureTools.Visibility = Visibility.Collapsed;
        }

        
        // Handles drawing the measure tools graphic
        private void myMeasureObject_DrawComplete(object sender, ESRI.ArcGIS.Client.DrawEventArgs args)
        {
            // Enables the drawing of the measuring graphic or point
            myMeasureObject.IsEnabled = true;
            // Removes previously drawn graphics
            graphicsLayer.ClearGraphics();

            // Initializes a new graphic
            Graphic graphic = new Graphic()
            {
                Geometry = args.Geometry,
                // Set the active symbol
                Symbol = _activeSymbol
            };

            // Adds the drawn graphic to the map
            graphicsLayer.Graphics.Add(graphic);

            if (symbology == "Point")
            {
                txtBlkMeasurement.Text = "Map Coords: X=" + Convert.ToString(graphic.Geometry.Extent.XMax) + ", Y= " + Convert.ToString(graphic.Geometry.Extent.YMax);
            }

            if (symbology == "Polygon")
            {
                txtBlkMeasurement.Text = "";
                geometryService.AreasAndLengthsCompleted += GeometryService_AreasAndLengthsCompleted;
                geometryService.AreasAndLengthsAsync(graphicsLayer.Graphics.ToList());
            }

            if (symbology == "Polyline")
            {
                txtBlkMeasurement.Text = "";
                geometryService.LengthsCompleted += GeometryService_LengthsCompleted;
                geometryService.LengthsAsync(graphicsLayer.Graphics.ToList());
            }

            // Disable the drawing of additional graphics
            myMeasureObject.IsEnabled = false;
        }

        // Measure Polygon
        private void GeometryService_AreasAndLengthsCompleted(object sender, AreasAndLengthsEventArgs args)
        {
            for (int i = 0; i < args.Results.Areas.Count; i++)
            {
                txtBlkMeasurement.Text = string.Format("Area = {1} sq. feet, \nPerimeter = {2} feet", i, Math.Abs(args.Results.Areas[i]).ToString("#0.00"), Math.Abs(args.Results.Lengths[i]).ToString("#0.00"));
            }
        }

        // Measure Line
        private void GeometryService_LengthsCompleted(object sender, LengthsEventArgs args)
        {
            for (int i = 0; i < args.Results.Count; i++)
            {
                txtBlkMeasurement.Text = string.Format("Length = {1} feet", i, args.Results[i].ToString("#0.00"));
            }
        }


        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            txtBlkMeasurement.Text = "";
            // Enables drawing
            myMeasureObject.IsEnabled = true;
            // Set the drawing to a point
            myMeasureObject.DrawMode = DrawMode.Point;
            // Sets the active symbol to the defined red point
            _activeSymbol = LayoutRoot.Resources["RedPoint"] as Symbol;
            symbology = "Point";

                    
        }

        private void btnClearGraphics_Click(object sender, RoutedEventArgs e)
        {
            graphicsLayer.ClearGraphics();
            txtBlkMeasurement.Text = "";
        }

        private void btnPolygon_Click(object sender, RoutedEventArgs e)
        {
            txtBlkMeasurement.Text = "";
            // Enables drawing
            myMeasureObject.IsEnabled = true;
            // Set the drawing to a polygon
            myMeasureObject.DrawMode = DrawMode.Polygon;
            // Sets the active symbol to a polygon
            _activeSymbol = LayoutRoot.Resources["BlueFillSymbol"] as FillSymbol;
            symbology = "Polygon";
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            txtBlkMeasurement.Text = "";
            // Enables drawing
            myMeasureObject.IsEnabled = true;
            // Set the drawing to a line
            myMeasureObject.DrawMode = DrawMode.Polyline;
            // Set the active symbol to a polyline
            _activeSymbol = LayoutRoot.Resources["BlueLineSymbol"] as CartographicLineSymbol;
            symbology = "Polyline";
        }

      
                
    }
}
