using GMap.NET;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Data.Common;

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageFM.xaml
    /// </summary>
    public partial class PageFM : Page
    {
        //Инициализация переменной индекса, объекта ЧС и БД
        public static int ind;
        PMobject obj = new PMobject();
        MySqlConnection myConnection;

        public PageFM(int num, MySqlConnection conn)
        {
            //Инициализация страницы с выгрузкой необходимых данных из БД
            InitializeComponent();
            ind = num;
            myConnection = conn;

            try
            {
                //СОставление запроса к БД
                string sql = "SELECT ObjectName FROM CSobject WHERE CategoryID = " + ind;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                List<string> list = new List<string>();

                //Чтение ответа БД
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            string csObjectName = reader.GetString(0);
                            list.Add(csObjectName);
                        }

                    }
                }

                //Запись ответа в текстовые блоки
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
                MessageBox.Show("Потеряно соединение с базой данных");
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
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    string sql = "SELECT ID FROM CSobject WHERE CategoryID = " + ind+" LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                    int id = -1;

                    //Чтение ответа БД
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

                    sql = "SELECT ObjectName,Vedomstvo,Subordination,IsReady,Num,Place,Phone,Latitude,Longitude FROM CSobject WHERE ID = " + obj.Number;
                    cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД
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
                    PageFMInfo pageFMInfo = new PageFMInfo(obj);
                    NavigationService.Navigate(pageFMInfo);

                    try
                    {
                        GMapControl map = (GMapControl)App.Current.MainWindow.FindName("mapView");
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
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось отобразить точку объекта");
                    }
                }
                catch
                {
                    MessageBox.Show("Потеряно соединение с базой данных");
                }
                
            }
        }
    }
}
