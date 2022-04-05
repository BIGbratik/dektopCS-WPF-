using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections.Generic;
using System.Data.Common;

namespace dektopCS
{
    public partial class MainWindow : Window
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection;
        public MainWindow()
        {
            InitializeComponent();
            //Подключение к базе данных
            MySqlConnection conn = DBconnection.GetDBConnection();
            myConnection = conn;
            //Добавление параметров подключения в ресурсы приложения
            App.Current.Resources["connectionMySQL"] = conn;
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT Roll FROM Users WHERE BINARY Login = '"+login.Text+ 
                    "' AND BINARY Passwd = '" + SecureStringToString(pwd.SecurePassword)+"'";
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                List<int> list = new List<int>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            list.Add(reader.GetInt32(0));
                        }
                    }
                }
                if (list.Count==0)
                {
                    MessageBox.Show("Введён неверный логин или пароль!!!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    pwd.Clear();
                }
                else
                {
                    App.Current.Resources["roll"] = list[0];
                    pwd.Clear();
                    login.Clear();
                    WorkWindow workWindow = new WorkWindow();
                    workWindow.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Метод закрытия окна
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //Разрыв соединения с БД и закрытие окна
            DBconnection.CloseDBConnection((MySqlConnection)App.Current.Resources["connectionMySQL"]);
            Close();
        }

        private string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
