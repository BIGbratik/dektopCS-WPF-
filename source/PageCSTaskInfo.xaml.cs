using dektopCS.classes;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;
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

            //Чтение ответа построчно
            /*CSTask task = new CSTask();
            task.ID = id;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    //Построчное считывание ответа
                    while (reader.Read())
                    {
                        task.TaskName = reader.GetString(0);
                        if (!reader.IsDBNull(1))
                            task.EmergLat = reader.GetDouble(1);
                        if (!reader.IsDBNull(2))
                            task.EmergLng = reader.GetDouble(2);
                        if (!reader.IsDBNull(3))
                            task.dateTime = reader.GetDateTime(3);
                        if (!reader.IsDBNull(4))
                            task.EmergParams = reader.GetString(4);
                        if (!reader.IsDBNull(5))
                            task.EmergMeasures = reader.GetString(5);
                    }
                }
            }
            title.Text =task.TaskName;
            datetime.Text = task.dateTime.ToString();*/
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
