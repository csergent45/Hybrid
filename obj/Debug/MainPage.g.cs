﻿#pragma checksum "C:\Inetpub\wwwroot\hybrid\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "31351FB5CEB010EF822720FC3DB75CF8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace hybrid {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal ESRI.ArcGIS.Client.Map myMap;
        
        internal ESRI.ArcGIS.Client.Toolkit.ScaleLine scaleline1;
        
        internal ESRI.ArcGIS.Client.Toolkit.MapProgressBar mapProgressBar;
        
        internal ESRI.ArcGIS.Client.Toolkit.Bookmark bookmark1;
        
        internal System.Windows.Controls.Border sideBar;
        
        internal System.Windows.Controls.Expander expanderMain;
        
        internal System.Windows.Controls.StackPanel navTools;
        
        internal System.Windows.Controls.Button btnZoomIn;
        
        internal System.Windows.Controls.Image image1;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal System.Windows.Controls.Button btnZoomOut;
        
        internal System.Windows.Controls.Image imgZoomOut;
        
        internal System.Windows.Controls.TextBlock txtBlkZoomOut;
        
        internal System.Windows.Controls.Image imgPrevExtent;
        
        internal System.Windows.Controls.TextBlock txtBlkPrevExtent;
        
        internal System.Windows.Controls.Image imgNextExtent;
        
        internal System.Windows.Controls.TextBlock txtBlkNextExtent;
        
        internal System.Windows.Controls.Button btnFullExtent;
        
        internal System.Windows.Controls.Image imgFullExtent;
        
        internal System.Windows.Controls.TextBlock txtBlkFullExtent;
        
        internal System.Windows.Controls.Button btnPan;
        
        internal System.Windows.Controls.Image imgPan;
        
        internal System.Windows.Controls.TextBlock txtBlkPan;
        
        internal System.Windows.Controls.Image imgIdentify;
        
        internal System.Windows.Controls.TextBlock txtBlkIdentify;
        
        internal System.Windows.Controls.Image imgMeasure;
        
        internal System.Windows.Controls.TextBlock txtBlkMeasure;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/hybrid;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.myMap = ((ESRI.ArcGIS.Client.Map)(this.FindName("myMap")));
            this.scaleline1 = ((ESRI.ArcGIS.Client.Toolkit.ScaleLine)(this.FindName("scaleline1")));
            this.mapProgressBar = ((ESRI.ArcGIS.Client.Toolkit.MapProgressBar)(this.FindName("mapProgressBar")));
            this.bookmark1 = ((ESRI.ArcGIS.Client.Toolkit.Bookmark)(this.FindName("bookmark1")));
            this.sideBar = ((System.Windows.Controls.Border)(this.FindName("sideBar")));
            this.expanderMain = ((System.Windows.Controls.Expander)(this.FindName("expanderMain")));
            this.navTools = ((System.Windows.Controls.StackPanel)(this.FindName("navTools")));
            this.btnZoomIn = ((System.Windows.Controls.Button)(this.FindName("btnZoomIn")));
            this.image1 = ((System.Windows.Controls.Image)(this.FindName("image1")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.btnZoomOut = ((System.Windows.Controls.Button)(this.FindName("btnZoomOut")));
            this.imgZoomOut = ((System.Windows.Controls.Image)(this.FindName("imgZoomOut")));
            this.txtBlkZoomOut = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkZoomOut")));
            this.imgPrevExtent = ((System.Windows.Controls.Image)(this.FindName("imgPrevExtent")));
            this.txtBlkPrevExtent = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkPrevExtent")));
            this.imgNextExtent = ((System.Windows.Controls.Image)(this.FindName("imgNextExtent")));
            this.txtBlkNextExtent = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkNextExtent")));
            this.btnFullExtent = ((System.Windows.Controls.Button)(this.FindName("btnFullExtent")));
            this.imgFullExtent = ((System.Windows.Controls.Image)(this.FindName("imgFullExtent")));
            this.txtBlkFullExtent = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkFullExtent")));
            this.btnPan = ((System.Windows.Controls.Button)(this.FindName("btnPan")));
            this.imgPan = ((System.Windows.Controls.Image)(this.FindName("imgPan")));
            this.txtBlkPan = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkPan")));
            this.imgIdentify = ((System.Windows.Controls.Image)(this.FindName("imgIdentify")));
            this.txtBlkIdentify = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkIdentify")));
            this.imgMeasure = ((System.Windows.Controls.Image)(this.FindName("imgMeasure")));
            this.txtBlkMeasure = ((System.Windows.Controls.TextBlock)(this.FindName("txtBlkMeasure")));
        }
    }
}

