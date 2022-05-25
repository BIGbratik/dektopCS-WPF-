using GMap.NET;
using GMap.NET.WindowsPresentation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dektopCS.source
{
    public partial class PageCSTasks : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];

        DispatcherTimer timer = new DispatcherTimer();
        private readonly int id;
        public PageCSTasks(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT ID,TaskName FROM EmergTasks WHERE EmergTypeID = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД
                List<string> list = new List<string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0)+" | "+reader.GetString(1));
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
                else if (lb.Items.Count==0)
                {
                    lb.Items.Add("Активных задач пока нет");
                    lb.IsEnabled = false;
                }
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

        private void AddCS_Click(object sender, RoutedEventArgs e)
        {
            PageCSAdd pageCSAdd = new PageCSAdd(id);
            NavigationService.Navigate(pageCSAdd);
        }

        //Получение списка задач по выбранному ЧС (по клику мыши)
        private void CheckTask_Click(object sender, RoutedEventArgs e)
        {
            /*timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();
            ProgressBar bar = new ProgressBar()
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
            //Проверка, выбран ли какой-либо пункт
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    TextBlock obj = (TextBlock)lb.Items[lb.SelectedIndex];
                    string[] str = obj.Text.Split('|');
                    //Открытие страницы с подробными данными о выбранном объекте
                    PageCSTaskInfo taskInfo = new PageCSTaskInfo(Convert.ToInt32(str[0]));
                    NavigationService.Navigate(taskInfo);
                }
                catch
                {
                    MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Получение списка задач по выбранному ЧС (по касанию экрана)
        private void CheckTask_TouchUp(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
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
            if (lb.SelectedIndex != -1)
            {
                try
                {
                    TextBlock obj = (TextBlock)lb.Items[lb.SelectedIndex];
                    string[] str = obj.Text.Split('|');
                    //Открытие страницы с подробными данными о выбранном объекте
                    PageCSTaskInfo taskInfo = new PageCSTaskInfo(Convert.ToInt32(str[0]));
                    NavigationService.Navigate(taskInfo);
                }
                catch
                {
                    MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Метод, вызываемый по тику таймера (заполнение прогресс бара)
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            /*if (taskProgres.Value<100)
            {
                taskProgres.Value += 1;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Задача успешно выполнена","Успех",MessageBoxButton.OK,MessageBoxImage.Information);
            }*/
        }
    }


}
