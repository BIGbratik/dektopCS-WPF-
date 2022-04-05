using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace dektopCS.source
{
    public partial class PageMPSinfo : Page
    {
        //Инициализация БД и пути к локальным данным
        private string path = @"/data/MPS/";

        private readonly string type;

        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        public PageMPSinfo(string type)
        {
            InitializeComponent();
            path =path+type+"/";
            this.type = type;
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //СОставление запроса к БД
                string sql = "SELECT MPSfile FROM MPS WHERE MPStype = '" + type + "' AND ObjectID IS NULL";
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
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Uri uri = new Uri(path+fName, UriKind.RelativeOrAbsolute);
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
                string fName = lb.SelectedItem.ToString();
                Uri uri = new Uri(path + fName, UriKind.RelativeOrAbsolute);
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

