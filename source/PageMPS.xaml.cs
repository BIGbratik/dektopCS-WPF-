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
    /// Логика взаимодействия для PageMPS.xaml
    /// </summary>
    public partial class PageMPS : Page
    {
        //Инициализация БД, пути к локальным данным, списка для нужных данных
        MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        private string path = @"/data/MPS/";
        List<string> types = new List<string>();
        public PageMPS(int id)
        {
            //Инициализация страницы и заполнение её выбранным перечнем данных
            InitializeComponent();
            
            try
            {
                //СОставление запроса к БД
                string sql = "SELECT MPStype,MPSfile FROM MPS WHERE ObjectID IS NOT NULL AND ObjectID = " + id;
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
                            string mpsType = reader.GetString(0);
                            types.Add(mpsType);
                            string mpsFiles = reader.GetString(1);
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
                path += types[lb.SelectedIndex] + "/"+ lb.SelectedItem.ToString();
                Image image = new Image();
                Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                img.Source = bitmapImage;

                lb.SelectedIndex = -1;
            }
        }

        private void CheckItem_TouchUp(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                path += types[lb.SelectedIndex] + "/" + lb.SelectedItem.ToString();
                Image image = new Image();
                Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                img.Source = bitmapImage;

                lb.SelectedIndex = -1;
            }
        }
    }
}
