using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace dektopCS.source
{
    public partial class PageCSAdd : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        public PageCSAdd(int id)
        {
            InitializeComponent();
            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT EmergTypeName FROM EmergType WHERE ID = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                string str = "";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            str = reader.GetString(0);
                        }
                    }
                }
                CSType.Content= str;
                CSData.Content = (DateTime.UtcNow.AddHours(3)).ToString("G");
            }
            catch
            {
                MessageBox.Show("Потеряно соединение с базой данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция находится в разработке", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
