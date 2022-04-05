using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;

namespace dektopCS.source
{
    public partial class PageAnalytics : Page
    {
        //Инициализация пути к статическим ресурсам
        private readonly string path = @".\data\Analityc\";

        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        public PageAnalytics()
        {
            InitializeComponent();
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Составление и отправка запроса к БД
                string sql = "SELECT AnalyticName FROM Analytic";
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
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Метод возвращения ан предыдущую страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Запуск аналитической программы
        private void StartProgram_Click(object sender, RoutedEventArgs e)
        {
            //Запуск только при выборе какого-либо элемента
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    //Составление и отправка запроса к БД
                    string sql = "SELECT AnalyticFile FROM Analytic WHERE ID = " + (lb.SelectedIndex + 1);
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД построчно
                    string analyticFile = "";
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //Построчное считывание ответа
                            while (reader.Read())
                            {
                                analyticFile = reader.GetString(0);
                            }
                        }
                    }
                    try
                    {
                        Process.Start($"{path}" + analyticFile);
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Потеряно соединение с базой данных","Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void StartProgram_TouchUp(object sender, RoutedEventArgs e)
        {
            //Запуск только при выборе какого-либо элемента
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    //Составление и отправка запроса к БД
                    string sql = "SELECT AnalyticFile FROM Analytic WHERE ID = " + (lb.SelectedIndex + 1);
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД построчно
                    string analyticFile = "";
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //Построчное считывание ответа
                            while (reader.Read())
                            {
                                analyticFile = reader.GetString(0);
                            }
                        }
                    }
                    try
                    {
                        Process.Start($"{path}" + analyticFile);
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Потеряно соединение с базой данных", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
