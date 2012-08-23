﻿using System;
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
            _toolMode = "zoomin";
        }


        private void myDrawObject_DrawComplete(object sender, DrawEventArgs args)
        {
            if (_toolMode == "zoomin")
            {
                myMap.ZoomTo(args.Geometry as ESRI.ArcGIS.Client.Geometry.Envelope);
            }
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
            MyDrawObject.IsEnabled = true;
            _toolMode = "zoomout";

        }

        private void btnPan_Click(object sender, RoutedEventArgs e)
        {
            MyDrawObject.IsEnabled = false;
            _toolMode = "";
        }

        private void btnFullExtent_Click(object sender, RoutedEventArgs e)
        {
            myMap.ZoomTo(myMap.Layers.GetFullExtent());
        }
                
    }
}
