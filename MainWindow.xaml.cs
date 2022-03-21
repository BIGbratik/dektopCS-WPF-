using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
        public MySqlConnection conn;
        //Координаты стартового положения центра карты
        public double startLat = 55.796127;
        public double startLng = 49.106414;
        public MainWindow()
        {
            InitializeComponent();
            //Формирование фонового изображения окна
            Uri mcs = new Uri(BaseUriHelper.GetBaseUri(this), "data/ЗвездаМЧС.png");
            BitmapImage image = new BitmapImage(mcs);
            ImageBrush img = new ImageBrush(image);
            this.Background = img;
        }

        //Метод отрисовки карты при загрузке окна
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {

            //Установление соединения с БД
            conn = myStringConn.GetDBConnection();
            App.Current.Resources["connectionMySQL"] = conn;

            if (InternetAvailability.IsInternetAvailable())
            {
                //Отрисовка карты с центро в г.Казань
                mapView.MapProvider = GoogleMapProvider.Instance;
                mapView.MinZoom = 4;
                mapView.MaxZoom = 18;
                mapView.Zoom = 10;
                mapView.Position = new PointLatLng(startLat, startLng);
                mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
                mapView.CanDragMap = true;
                mapView.DragButton = MouseButton.Left;
                mapView.ShowCenter = false;
                mapView.ShowTileGridLines = false;

                //Создание строки запроса к Я.Погоде и добавление её в ресурс WebBrowser
                try
                {
                    string weatherPath = "https://yandex.ru/pogoda/?lat=" + startLat.ToString("G", CultureInfo.InvariantCulture) + "&lon=" + startLng.ToString("G", CultureInfo.InvariantCulture);
                    Uri weaterUri = new Uri(weatherPath, UriKind.RelativeOrAbsolute);
                    weather.Source = weaterUri;
                }
                catch
                {
                    MessageBox.Show("Внешний ресурс ПОГОДА недоступен. Возможно отсутствует соединение с сетью Интернет", "Ошибка соединения", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Кажется отсутствует соединение с сетью Интернет, либо оно слабое\n" +
                    "Для доступа к некоторым функциям приложения требуется наличие подключения к сети. " +
                    "Устарните проблемы и перезапустите приложение", "Ошибка сети", MessageBoxButton.OK, MessageBoxImage.Warning);
                grid.Children.Remove(weather);
                grid.Children.Remove(mapView);

            }

        }

        //Метод закрытия окна
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            myStringConn.CloseDBConnection(conn);
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
