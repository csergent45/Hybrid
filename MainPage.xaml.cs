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




namespace hybrid
{
    public partial class MainPage : UserControl
    {
        string _toolMode = "";
        List<Envelope> _extentHistory = new List<Envelope>();
        int _currentExtentIndex = 0;
        bool _newExtent = true;

        private Draw MyDrawObject;

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


        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            MyDrawObject.IsEnabled = true;
            // Zoom Map In
            _toolMode = "zoomin";
            // Change the background color of a button on click - http://forums.silverlight.net/p/259771/648943.aspx/1?How+to+change+button+colour+on+mouseclick+
            btnZoomIn.Background = new SolidColorBrush(Colors.White);
                        
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

        }

        private void btnPan_Click(object sender, RoutedEventArgs e)
        {
            // Pan Map
            MyDrawObject.IsEnabled = false;
            _toolMode = "";
            btnPan.Background = new SolidColorBrush(Colors.White);
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

        }
                
    }
}
