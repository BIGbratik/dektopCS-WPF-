using System;
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

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapProvider = GoogleMapProvider.Instance;
            mapView.MinZoom = 1;
            mapView.MaxZoom = 16;
            mapView.Zoom = 10;
            mapView.Position = new GMap.NET.PointLatLng(55.796127, 49.106414);
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            mapView.CanDragMap = true;
            mapView.DragButton = MouseButton.Left;
            mapView.ShowCenter = false;
            mapView.ShowTileGridLines = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            /*if (rightPlace.Visibility == Visibility.Visible)
            {
                rightPlace.Visibility = Visibility.Collapsed;
            }
            else
            {
                rightPlace.Visibility = Visibility.Visible;
            }*/
        }
    }
}
