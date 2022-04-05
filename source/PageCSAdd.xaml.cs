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

        private readonly int EmergTypeID;
        public PageCSAdd(int id)
        {
            InitializeComponent();
            EmergTypeID = id;
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT EmergTypeName FROM EmergType WHERE ID = " + EmergTypeID;
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
                CSType.Content = str;
                //CSDate.Content = (DateTime.Now).ToString("G");
                CSDate.Text = (DateTime.Now).ToString("G");
            }
            catch
            {
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "INSERT EmergTasks(EmergTypeID, TaskName, EmergTime) VALUE (" + EmergTypeID+",'"+ CSName.Text + "',@dt)";

                MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                cmd.Parameters.AddWithValue("@dt", DateTime.Parse(CSDate.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Сохранение прошло успешно","Успех!!!",MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения","Внимание!!!",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
    }
}
