using GMap.NET;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Common;

namespace dektopCS.source
{
    public partial class PageFM : Page
    {
        //Инициализация переменной индекса, объекта ЧС и БД
        private readonly int id;
        private PMobject obj = new PMobject();

        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];

        public PageFM(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Составление и отправка запроса к БД
                string sql = "SELECT ObjectName FROM CSobject WHERE CategoryID = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа построчно
                List<string> list = new List<string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }

                    }
                }

                //Формирование полученных данных в правильные блоки отображения
                List<TextBlock> tb = new List<TextBlock>();
                for (int i = 0; i < list.Count; i++)
                {
                    tb.Add(new TextBlock
                    {
                        Text = list[i],
                        TextWrapping = TextWrapping.WrapWithOverflow
                    });
                }
                lb.ItemsSource = tb;
            }
            catch
            {
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Возвращение на предыдущую страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Переход к странице подробной информации об выбранном объекте СиС и постановка на карте метки выбранного объекта
        private void CheckInfo_Click(object sender, RoutedEventArgs e)
        {
            //Проверка, выбран ли какой-либо пункт
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    //Формирование и отпарвка запроса к БД
                    string sql = "SELECT ID FROM CSobject WHERE CategoryID = " + id+" LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД построчно
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //Построчное считывание ответа
                            while (reader.Read())
                            {
                                obj.Number = reader.GetInt32(0) + lb.SelectedIndex;
                            }

                        }
                    }

                    //Формирование и отпарвка запроса к БД
                    sql = "SELECT ObjectName,Vedomstvo,Subordination,IsReady,Num,Place,Phone,Latitude,Longitude FROM CSobject WHERE ID = " + obj.Number;
                    cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД построчно
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //Построчное считывание ответа
                            while (reader.Read())
                            {
                                obj.Name = reader.GetString(0);
                                obj.Structer = reader.GetString(1);
                                obj.Subord = reader.GetString(2);
                                obj.isReady = reader.GetString(3);
                                obj.Count = reader.GetString(4);
                                obj.Place = reader.GetString(5);
                                obj.Phone = reader.GetString(6);
                                obj.Latitude = reader.GetDouble(7);
                                obj.Longitude = reader.GetDouble(8);
                            }
                        }
                    }
                    //Открытие страницы с подробными данными о выбранном объекте
                    PageFMInfo pageFMInfo = new PageFMInfo(obj);
                    NavigationService.Navigate(pageFMInfo);

                    //Поставка метки выбранного объекта на карту
                    try
                    {
                        GMapControl map = (GMapControl)App.Current.Windows[1].FindName("mapView");
                        App.Current.Resources["markerPath"] = "";

                        GMapMarker marker = new GMapMarker(new PointLatLng(obj.Latitude, obj.Longitude));

                        Ellipse img = new Ellipse()
                        {
                            Width = 20,
                            Height = 20,
                            ToolTip = obj.Name,
                            Fill = Brushes.MidnightBlue,
                            Stroke = Brushes.DarkOrange,
                            StrokeThickness = 3
                        };
                        marker.Shape = img;
                        map.Markers.Add(marker);
                        map.Position = new PointLatLng(obj.Latitude, obj.Longitude);
                        map.Zoom = 18;
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось отобразить точку объекта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
       
        private void CheckInfo_TouchUp(object sender, RoutedEventArgs e)
        {
            //Проверка, выбран ли какой-либо пункт
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    //Формирование и отпарвка запроса к БД
                    string sql = "SELECT ID FROM CSobject WHERE CategoryID = " + id + " LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД построчно
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //Построчное считывание ответа
                            while (reader.Read())
                            {
                                obj.Number = reader.GetInt32(0) + lb.SelectedIndex;
                            }

                        }
                    }

                    //Формирование и отпарвка запроса к БД
                    sql = "SELECT ObjectName,Vedomstvo,Subordination,IsReady,Num,Place,Phone,Latitude,Longitude FROM CSobject WHERE ID = " + obj.Number;
                    cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД построчно
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //Построчное считывание ответа
                            while (reader.Read())
                            {
                                obj.Name = reader.GetString(0);
                                obj.Structer = reader.GetString(1);
                                obj.Subord = reader.GetString(2);
                                obj.isReady = reader.GetString(3);
                                obj.Count = reader.GetString(4);
                                obj.Place = reader.GetString(5);
                                obj.Phone = reader.GetString(6);
                                obj.Latitude = reader.GetDouble(7);
                                obj.Longitude = reader.GetDouble(8);
                            }
                        }
                    }
                    //Открытие страницы с подробными данными о выбранном объекте
                    PageFMInfo pageFMInfo = new PageFMInfo(obj);
                    NavigationService.Navigate(pageFMInfo);

                    //Поставка метки выбранного объекта на карту
                    try
                    {
                        GMapControl map = (GMapControl)App.Current.Windows[1].FindName("mapView");
                        App.Current.Resources["markerPath"] = "";

                        GMapMarker marker = new GMapMarker(new PointLatLng(obj.Latitude, obj.Longitude));

                        Ellipse img = new Ellipse()
                        {
                            Width = 20,
                            Height = 20,
                            ToolTip = obj.Name,
                            Fill = Brushes.MidnightBlue,
                            Stroke = Brushes.DarkOrange,
                            StrokeThickness = 3
                        };
                        marker.Shape = img;
                        map.Markers.Add(marker);
                        map.Position = new PointLatLng(obj.Latitude, obj.Longitude);
                        map.Zoom = 18;
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось отобразить точку объекта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
