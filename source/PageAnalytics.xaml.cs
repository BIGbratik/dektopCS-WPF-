using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
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

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageAnalytics.xaml
    /// </summary>
    public partial class PageAnalytics : Page
    {
        //Инициализация пути к статическим ресурсам
        string path = @".\data\Analityc\";
        MySqlConnection myConnection;
        public PageAnalytics(MySqlConnection conn)
        {
            //Инициализация страницы с выгрузкой данных из БД
            InitializeComponent();

            //Установление соединения с MySQL бд
            myConnection = conn;
            try
            {
                //СОставление запроса к БД
                string sql = "SELECT AnalyticName FROM Analytic";
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
                            string analyticName = reader.GetString(0);
                            list.Add(analyticName);
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

        //Метод возвращения ан предыдущую страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Запуск аналитической программы
        private void StartProgram_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                //Установление соединения с MySQL бд
                try
                {
                    //СОставление запроса к БД
                    string sql = "SELECT AnalyticFile FROM Analytic WHERE ID = " + (lb.SelectedIndex + 1);
                    string analyticFile="";
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                    //Чтение ответа БД
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
                        MessageBox.Show("Не удаётся запустить выбранную программу");
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
