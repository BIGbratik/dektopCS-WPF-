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
        //Инициализация БД и пути к статическим ресурсам
        desktopDBEntities1 db;
        string path = @".\data\Analityc\";
        public PageAnalytics()
        {
            //Инициализация страницы с выгрузкой данных из БД
            InitializeComponent();

            MySqlConnection myConnection = myStringConn.GetDBConnection();
            myConnection.Open();
            try
            {
                string sql = "SELECT AnalyticName FROM desktopdb.analytic";
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                List<string> list = new List<string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string analyticFile = reader.GetString(0);
                            list.Add(analyticFile);
                        }

                    }
                }
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
                MessageBox.Show("Connection lost");
            }
            finally
            {
                myConnection.Close();
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
            if (lb.SelectedIndex!=-1)
            {
                string fName = db.Analytic.Where(a => a.ID.Equals(lb.SelectedIndex + 1)).Select(b => b.AnalyticFile).FirstOrDefault();
                Process.Start($"{path}" + fName);
                
            }

        }
    }
}
