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
            mapView.Zoom = 11;
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
        }

        private void CheckForcMeans_Click(object sender, RoutedEventArgs e)
        {
            WindowForceMeans forceMeans = new WindowForceMeans();
            forceMeans.Show();
        }
        private void CheckMaps_Click(object sender, RoutedEventArgs e)
        {
            WindowMaps windowMaps = new WindowMaps();
            windowMaps.Show();
        }
        private void CheckAnalytics_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль АНАЛИТИКА находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckGuids_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль СПРАВОЧНИКИ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckMultimedia_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль МУЛЬТИМЕДИА находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
