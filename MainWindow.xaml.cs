using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;


namespace dektopCS
{
    public partial class MainWindow : Window
    {
        //Координаты стартового положения центра карты
        public double startLat = 55.796127;
        public double startLng = 49.106414;
        public MainWindow()
        {
            InitializeComponent();
            //Подключение к базе данных
            MySqlConnection conn = DBconnection.GetDBConnection();
            //Добавление параметров подключения в ресурсы приложения
            App.Current.Resources["connectionMySQL"] = conn;
        }

        //Метод отрисовки карты при загрузке окна
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            //Проверка соединения с сетью Интернет
            if (InternetAvailability.IsInternetAvailable())
            {
                //Отрисовка карты с центро в г.Казань
                mapView.MapProvider = GoogleMapProvider.Instance;
                mapView.MinZoom = 4;
                mapView.MaxZoom = 20;
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
                    string weatherPath = "https://yandex.ru/pogoda/?lat=" + 
                        startLat.ToString("G", CultureInfo.InvariantCulture) + 
                        "&lon=" + startLng.ToString("G", CultureInfo.InvariantCulture);
                    Uri weaterUri = new Uri(weatherPath, UriKind.RelativeOrAbsolute);
                    weather.Source = weaterUri;
                }
                catch
                {
                    MessageBox.Show("Внешний ресурс ПОГОДА недоступен. Возможно отсутствует соединение с сетью Интернет", 
                        "Ошибка соединения", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Кажется отсутствует соединение с сетью Интернет, либо оно слабое\n" +
                    "Для доступа к некоторым функциям приложения требуется наличие подключения к сети. " +
                    "Устарните проблемы и перезапустите приложение", "Ошибка сети", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Удаление объектов, для работы которых требуется соединение с сетью интернет
                grid.Children.Remove(weather);
                grid.Children.Remove(mapView);
            }
        }

        //Метод закрытия окна
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //Разрыв соединения с БД и закрытие окна
            DBconnection.CloseDBConnection((MySqlConnection)App.Current.Resources["connectionMySQL"]);
            Close();
        }

        //Приближение
        private void ZoomUP_Click(object sender, RoutedEventArgs e)
        {
            mapView.Zoom += 1;
        }

        //Отдаление
        private void ZoomDOWN_Click(object sender, RoutedEventArgs e)
        {
            mapView.Zoom -= 1;
        }

        //Метод постановки метки на карту по ПКМ
        void MapView_RightButtonDown(object sender, MouseEventArgs e)
        {
            //Проверка на наличие данных о метке в ресурсах
            if (App.Current.Resources["markerPath"]!=null)
            {
                //Получение, подготовка и вставка изображения метки на карту
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
