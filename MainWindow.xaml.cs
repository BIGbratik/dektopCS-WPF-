﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
//using GMap.NET.WindowsForms;
//using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;

namespace dektopCS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void drowObject(string str)
        {
            string[] pathStr = str.Split('|');
            double lat = Convert.ToDouble(pathStr[0]);
            double lng = Convert.ToDouble(pathStr[1]);
            string tip = pathStr[2];

            GMapMarker marker = new GMapMarker(new PointLatLng(lat, lng));

            Ellipse img = new Ellipse()
            {
                Width = 20,
                Height = 20,
                ToolTip = tip,
                Fill = Brushes.MidnightBlue,
                Stroke=Brushes.DarkOrange,
                StrokeThickness=3

            };

            marker.Shape = img;
            mapView.Markers.Add(marker);
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapProvider = GoogleMapProvider.Instance;
            mapView.MinZoom = 4;
            mapView.MaxZoom = 16;
            mapView.Zoom = 10;
            mapView.Position = new PointLatLng(55.796127, 49.106414);
            mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mapView.CanDragMap = true;
            mapView.DragButton = MouseButton.Left;
            mapView.ShowCenter = false;
            mapView.ShowTileGridLines = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //ДОРАБОТАТЬ!!!!!!!!!!!!!!!!
        void mapView_RightButtonDown(object sender, MouseEventArgs e)
        {
            if (App.Current.Resources["markerPath"]!=null)
            {
                string path = @"/data/Marks/" + App.Current.Resources["markerPath"];
                GMapMarker marker = new GMapMarker(mapView.FromLocalToLatLng((int)e.GetPosition(mapView).X, (int)e.GetPosition(mapView).Y));
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();

                Image img = new Image()
                {
                    Source = bitmapImage,
                    Width = 20,
                    Height = 20,
                    ToolTip = "Тестовая метка",

                };

                marker.Shape = img;
                mapView.Markers.Add(marker);
            }
        }

    }
}
