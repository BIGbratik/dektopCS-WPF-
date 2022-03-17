using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageClassificators.xaml
    /// </summary>
    public partial class PageClassificators : Page
    {
        //Инициализации БД и объекта будужщей страницы
        MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        object page;
        public PageClassificators()
        {
            //Инициализация страницы с выгрузкой необходимых данных из БД
            InitializeComponent();

            try
            {
                //СОставление запроса к БД
                string sql = "SELECT EmergName FROM Emerg WHERE ID = 1";
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                string left = "";

                //Чтение ответа БД
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

                sql = "SELECT EmergName FROM Emerg WHERE ID = 2";
                cmd = new MySqlCommand(sql, myConnection);
                string right = "";

                //Чтение ответа БД
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
                //СОставление запроса к БД
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

            TextBlock tb = new TextBlock();
            tb.Text = "Виды ЧС";
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.FontSize = 20;
            tb.Foreground = Brushes.White;

            Button returnBtn = new Button();
            returnBtn.Content = "Вернуться к выбору характера ЧС";
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
