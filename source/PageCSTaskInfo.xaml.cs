using dektopCS.classes;
using GMap.NET.WindowsPresentation;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
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

namespace dektopCS.source
{
    public partial class PageCSTaskInfo : Page
    {
        public readonly int id;
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        public PageCSTaskInfo(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Заполнение страницы заданиями события и их состояния
            //Составление и отправка запроса к БД
            string sql = "SELECT TaskName,EmergLat,EmergLng,EmergTime,EmergParams,EmergMeasures FROM EmergTasks WHERE ID = " + id;
            MySqlCommand cmd = new MySqlCommand(sql, myConnection);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    //Построчное считывание ответа
                    while (reader.Read())
                    {
                        title.Text = reader.GetString(0);
                        if (!reader.IsDBNull(3))
                            datetime.Text = reader.GetDateTime(3).ToString();
                        if (!reader.IsDBNull(4))
                            description.Text += "Параметры события: "+reader.GetString(4);
                        if (!reader.IsDBNull(5))
                            description.Text += "\nПринятые меры: "+reader.GetString(5);
                    }
                }
            }

            sql = "SELECT Name FROM Files WHERE TaskID = " + id;
            cmd=new MySqlCommand(sql, myConnection);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    //Построчное считывание ответа
                    while (reader.Read())
                    {
                        TextBlock tb = new TextBlock()
                        {
                            Text = reader.GetString(0),
                            Foreground = Brushes.Black,
                            TextWrapping=TextWrapping.WrapWithOverflow
                        };
                        ld.Items.Add(tb);
                    }
                }
            }
            if (ld.Items.Count == 0)
            {
                TextBlock tb = new TextBlock()
                {
                    Text = "Отсутствуют связанные документы",
                    Foreground = Brushes.Black
                };
                ld.Items.Add(tb);
                ld.IsEnabled = false;
            }
            else
            {
                ld.IsEnabled = true;
            }    
            //Поставка метки выбранного объекта на карту
            /*try
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
            }*/
        }

        //Возврат на предыдущю страницу и удаление метки объекта с карты
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            /*GMapControl map = (GMapControl)App.Current.Windows[1].FindName("mapView");
            App.Current.Resources["markerPath"] = "";
            map.Markers.Clear();
            map.Zoom = 10;*/
        }
    }
}
