using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageForceMeans.xaml
    /// </summary>
    public partial class PageForceMeans : Page
    {
        //Инициализация БД
        MySqlConnection myConnection;
        public PageForceMeans(MySqlConnection conn)
        {
            //Инициализация страницы и выгрузка необходимых данных из БД
            InitializeComponent();
            myConnection = conn;

            try
            {
                //СОставление запроса к БД
                string sql = "SELECT CategoryName FROM Category";
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
                            string categoryName = reader.GetString(0);
                            list.Add(categoryName);
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

        //Возврат на предыдущю страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Переход на страницу объектов СиС из выбранной категории
        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex!=-1)
            {
                PageFM pageFM = new PageFM(lb.SelectedIndex + 1,myConnection);
                NavigationService.Navigate(pageFM);
                lb.SelectedIndex = -1;
            }
        }
    }
}
