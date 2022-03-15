using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dektopCS
{
    public class myStringConn
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "desktopdb";
            string username = "root";
            string password = "LKV1";

            String connString = "server=" + host + ";port=" + port + ";database=" + database + ";user=" + username + ";password=" + password;
            MySqlConnection conn = new MySqlConnection(connString);

            return conn; 
        }
    }
}
