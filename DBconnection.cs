using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace dektopCS
{
    //Служебный класс, для подключения к бд MySQL
    public class DBconnection
    {
        //Метод подключения к БД с возможностью быстрого изменения параметров строки подключения
        public static MySqlConnection GetDBConnection()
        {
            string host = /*"192.168.43.106"*/"localhost";
            int port = 3306;
            string database = "desktopdb";
            string username = "root";
            string password = "LKV1";

            String connString = "server=" + host + ";port=" + port + ";database=" + database + ";user=" + username + ";password=" + password;
            MySqlConnection conn = new MySqlConnection(connString);
            //Открытие соединения
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Не удалось установить соединение с сервером БАЗЫ ДАННЫХ");
            }

            return conn; 
        }

        //Метод закрытия соединения с переданным в параметрах подключением
        public static void CloseDBConnection(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}
