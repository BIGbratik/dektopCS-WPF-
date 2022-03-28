using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Threading;

namespace dektopCS.source
{
    public partial class PageCSTasks : Page
    {
        MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];
        DispatcherTimer timer = new DispatcherTimer();
        public PageCSTasks(int id)
        {
            InitializeComponent();
            try
            {
                //СОставление запроса к БД
                string sql = "SELECT TaskName FROM EmergTasks WHERE EmergTypeID = "+id;
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
                            list.Add(reader.GetString(0));
                        }
                    }
                }

                if (list.Count > 0)
                {
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
                else
                {
                    lb.Items.Add("Активных задач пока нет");
                    lb.IsEnabled = false;
                }
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

        private void AddCS_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Получение списка задач по выбранному ЧС (по клику мыши)
        private void CheckTask_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();
            /*ProgressBar bar = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                //IsIndeterminate = true,
                Height = 50,
                Name = "progress"
                /orizontalAlignment=HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            TaskGrid.Children.Add(bar);
            Grid.SetRow(bar, 3);
            Grid.SetColumnSpan(bar, 2);*/
        }

        //Получение списка задач по выбранному ЧС (по касанию экрана)
        private void CheckTask_TouchUp(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();
            /*ProgressBar bar = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                //IsIndeterminate = true,
                Height = 50,
                Name = "progress"
                /orizontalAlignment=HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            TaskGrid.Children.Add(bar);
            Grid.SetRow(bar, 3);
            Grid.SetColumnSpan(bar, 2);*/
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (taskProgres.Value<100)
            {
                taskProgres.Value += 1;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Задача успешно выполнена","Успех",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
    }


}
