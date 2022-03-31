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
    public partial class PageMPS : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];

        private readonly string path = @"/data/MPS/";

        List<string> types = new List<string>();
        public PageMPS(int id)
        {
            InitializeComponent();
            
            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT MPStype,MPSfile FROM MPS WHERE ObjectID IS NOT NULL AND ObjectID = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                List<string> list = new List<string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            types.Add(reader.GetString(0));
                            list.Add(reader.GetString(1));
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
                Uri uri = new Uri(path+ types[lb.SelectedIndex] + "/" + lb.SelectedItem.ToString(), UriKind.RelativeOrAbsolute);
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
                Uri uri = new Uri(path + types[lb.SelectedIndex] + "/" + lb.SelectedItem.ToString(), UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                img.Source = bitmapImage;

                lb.SelectedIndex = -1;
            }
        }
}
