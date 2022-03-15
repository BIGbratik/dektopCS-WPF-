using System;
using System.Collections.Generic;
using System.Data.Common;
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
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;


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

        //Метод отрисовки карты при загрузке окна
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapProvider = GoogleMapProvider.Instance;
            mapView.MinZoom = 4;
            mapView.MaxZoom = 18;
            mapView.Zoom = 10;
            mapView.Position = new PointLatLng(55.796127, 49.106414);
            mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mapView.CanDragMap = true;
            mapView.DragButton = MouseButton.Left;
            mapView.ShowCenter = false;
            mapView.ShowTileGridLines = false;
        }

        //Метод закрытия окна
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Метод постановки метки на карту по ПКМ
        void mapView_RightButtonDown(object sender, MouseEventArgs e)
        {
            if (App.Current.Resources["markerPath"]!=null)
            {
                string path = @"/data/Marks/" + App.Current.Resources["markerPath"];
                GMapMarker marker = new GMapMarker(mapView.FromLocalToLatLng((int)e.GetPosition(mapView).X-15, (int)e.GetPosition(mapView).Y-15));
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();

                Image img = new Image()
                {
                    Source = bitmapImage,
                    Width = 30,
                    Height = 30,
                    ToolTip = App.Current.Resources["markerName"]
                };

                marker.Shape = img;
                mapView.Markers.Add(marker);
            }
        }
    }
}
