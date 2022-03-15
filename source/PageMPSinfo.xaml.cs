using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageMPSinfo.xaml
    /// </summary>
    public partial class PageMPSinfo : Page
    {
        //Инициализация БД и пути к локальным данным
        private string path = @"/data/MPS/";
        MySqlConnection myConnection;
        public PageMPSinfo(string type, MySqlConnection conn)
        {
            //Инициализация страницы и загрузка списка данных, касающихся выбранного объекта
            InitializeComponent();
            path =path+type+"/";
            myConnection = conn;
            
            try
            {
                //СОставление запроса к БД
                string sql = "SELECT MPSfile FROM MPS WHERE MPStype = '"+type+"' AND ObjectID IS NULL";
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
                            string mpsFiles = reader.GetString(0);
                            list.Add(mpsFiles);
                        }

                    }
                }

                //Запись ответа в текстовые блоки
                if (list.Count == 0)
                {
                    lb.Items.Add("В базе данных отсутствуют объекты, связанные с выбранным объектом");
                    lb.IsEnabled = false;
                }
                else
                {
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
                    lb.ItemsSource = list;
                    lb.IsEnabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Потеряно соединение с базой данных");
            }

        }

        //Возврат на предыдущю страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Отрисовка выбранного изображения
        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                string fName=lb.SelectedItem.ToString();
                Image image = new Image();
                Uri uri = new Uri(path+fName, UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                img.Source = bitmapImage;

                lb.SelectedIndex = -1;
            }
        }

        //ДОРАБОТАТЬ ПРИБЛИЖЕНИЕ КАРТИНКИ!!!
        private void Zoom_Wheel (object sender, RoutedEventArgs e)
        {
            /*img.Width = 200;
            img.Height = 200;*/
        }
    }
}

