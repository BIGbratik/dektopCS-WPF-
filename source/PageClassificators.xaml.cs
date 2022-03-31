using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace dektopCS.source
{
    public partial class PageClassificators : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        private object page;
        public PageClassificators()
        {
            InitializeComponent();

            try
            {
                //Составление и отправка запроса к БД
                string sql = "SELECT EmergName FROM Emerg WHERE ID = 1";
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                string left = "";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            left=reader.GetString(0);
                        }
                    }
                    BtnLeft.Content = left;
                }

                //Составление и отправка запроса к БД
                sql = "SELECT EmergName FROM Emerg WHERE ID = 2";
                cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД
                string right = "";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            right = reader.GetString(0);
                        }
                    }
                    BtnRight.Content = right;
                }
            }
            catch
            {
                MessageBox.Show("Потеряно соединение с базой данных");
            }
            page = Claasifs.Content;
        }

        //Возвращение на предыдущую страницу
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Просмотр информации по перовму типу ЧС
        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            ShowTypes(1);
        }

        //Просмотр информации по второму типу ЧС
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            ShowTypes(2);
        }

        //Метод отрисовки страницы в зависимости от выбранного характера ЧС
        private void ShowTypes (int id)
        {
            List<string> types = new List<string>();

            try
            {
                //Составление и отправка13 запроса к БД
                string sql = "SELECT EmergTypeName FROM EmergType WHERE EmergID = "+id;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            types.Add(reader.GetString(0));
                        }

                    }
                }
            }
            catch
            {
                MessageBox.Show("Потеряно соединение с базой данных");
            }

            Grid grid = new Grid();
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            row1.Height = new GridLength(40);
            row3.Height = new GridLength(40);

            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);

            TextBlock tb = new TextBlock
            {
                Text = "Виды ЧС",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20,
                Foreground = Brushes.White
            };

            Button returnBtn = new Button
            {
                Content = "Вернуться к выбору характера ЧС"
            };
            returnBtn.Click += ReturnBtn_Click;

            ListBox lb = new ListBox
            {
                Name = "lb",
                ItemsSource = types,
            };

            grid.Children.Add(returnBtn);
            grid.Children.Add(tb);
            grid.Children.Add(lb);
            
            Grid.SetRow(tb, 0);
            Grid.SetRow(lb, 1);
            Grid.SetRow(returnBtn, 2);
            
            Claasifs.Content = grid;
        }

        //Метод отрисовки стартового состояния окна
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            Claasifs.Content = page;
        }

        private void LB_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
