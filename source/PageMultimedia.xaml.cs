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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageMultimedia.xaml
    /// </summary>
    public partial class PageMultimedia : Page
    {
        //ИНициализация БД
        MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        public PageMultimedia()
        {
            //Инициализация страницы и выгрузка необходимых данных из БД
            InitializeComponent();

            List<string> fNames = new List<string>();
            List<string> marksNames = new List<string>();
            try
            {
                //СОставление запроса к БД
                string sql = "SELECT MarkName, MarkImage FROM Marks";
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
                            fNames.Add(reader.GetString(1));
                            marksNames.Add(reader.GetString(0));
                        }

                    }
                }
            }
            catch
            {
                MessageBox.Show("Потеряно соединение с базой данных");
            }
            lb.ItemsSource = GetSP(fNames,marksNames);
        }

        //Возврат на предыдущую страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Запоминание выбранной метки для карты
        private void ChooseItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                string markerPath="";
                string markerName="";

                try
                {
                    //СОставление запроса к БД
                    string sql = "SELECT MarkName, MarkImage FROM Marks WHERE ID = "+(lb.SelectedIndex+1);
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
                                markerName=reader.GetString(0);
                                markerPath=reader.GetString(1);
                            }
                        }
                        App.Current.Resources["markerPath"] = markerPath;
                        App.Current.Resources["markerName"] = markerName;
                    }
                }
                catch
                {
                    MessageBox.Show("Потеряно соединение с базой данных");
                }


            }
        }

        private void ChooseItem_TouchIUp(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                string markerPath = "";
                string markerName = "";

                try
                {
                    //СОставление запроса к БД
                    string sql = "SELECT MarkName, MarkImage FROM Marks WHERE ID = " + (lb.SelectedIndex + 1);
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
                                markerName = reader.GetString(0);
                                markerPath = reader.GetString(1);
                            }
                        }
                        App.Current.Resources["markerPath"] = markerPath;
                        App.Current.Resources["markerName"] = markerName;
                    }
                }
                catch
                {
                    MessageBox.Show("Потеряно соединение с базой данных");
                }


            }
        }
        //Метод составление списка, содержащего изображения меток и их названия
        public List<StackPanel> GetSP(List<string> fn, List<string>mn)
        {
            string path = "/data/Marks/";
            List<StackPanel> stackPanel = new List<StackPanel>();
            for (int i = 0; i < fn.Count; i++)
            {
                string img = path + fn[i];

                TextBlock tb = new TextBlock
                {
                    Text = mn[i],
                    TextWrapping= TextWrapping.WrapWithOverflow
                };
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.FontSize = 16;

                Image image = new Image();
                Uri uri = new Uri(img,   UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                image.Source = bitmapImage;
                image.Margin = new Thickness(5);

                StackPanel sp = new StackPanel();

                Grid grid = new Grid();

                ColumnDefinition column1 = new ColumnDefinition();
                column1.Width = new GridLength(60);

                ColumnDefinition column2 = new ColumnDefinition();
                
                RowDefinition row1= new RowDefinition();
                row1.Height = new GridLength(60);

                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                grid.RowDefinitions.Add(row1);

                grid.Children.Add(image);
                grid.Children.Add(tb);

                Grid.SetRow(image, 0);
                Grid.SetRow(tb, 0);
                Grid.SetColumn(image, 0);
                Grid.SetColumn(tb, 1);

                sp.Children.Add(grid);
                sp.Orientation=Orientation.Horizontal;

                stackPanel.Add(sp);
            }
            return stackPanel;
        }

        //Метод удаления всех поставленных меток на карту
        private void  Clear_Btn(object sender, RoutedEventArgs e)
        {
            GMapControl map = (GMapControl)App.Current.MainWindow.FindName("mapView");
            App.Current.Resources["markerPath"] = "";
            map.Markers.Clear();
        }
    }
}
