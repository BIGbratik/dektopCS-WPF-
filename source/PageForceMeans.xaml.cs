using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace dektopCS.source
{
    public partial class PageForceMeans : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        public PageForceMeans()
        {
            InitializeComponent();

            try
            {
                //Составление и отправка запроса к БД
                string sql = "SELECT CategoryName FROM Category";
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
                MessageBox.Show("Потеряно соединение с базой данных");
            }

        }

        //Возврат на предыдущю страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Переход на страницу объектов СиС из выбранной категории
        private void CheckItem_ClickUp(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex!=-1)
            {
                PageFM pageFM = new PageFM(lb.SelectedIndex+1);
                NavigationService.Navigate(pageFM);
                lb.SelectedIndex = -1;
            }
        }

        private void ChekItem_TouchUp(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                PageFM pageFM = new PageFM(lb.SelectedIndex + 1);
                NavigationService.Navigate(pageFM);
                lb.SelectedIndex = -1;
            }
        }
    }
}
