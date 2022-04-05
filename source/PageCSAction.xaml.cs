﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace dektopCS.source
{
    public partial class PageCSAction : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection =(MySqlConnection)App.Current.Resources["connectionMySQL"];

        private List<int> EmergID = new List<int>();
        public PageCSAction()
        {
            InitializeComponent();
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT EmergName FROM Emerg";
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                List<string> list = new List<string>();
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
                if(lbEmerg.Items.Count!=0)
                {
                    lbEmerg.Items.Clear();
                    lbEmerg.Items.Add("Не выбран тип ЧС");
                }

                lbEmerg.IsEnabled = false;
                lbType.ItemsSource = tb;
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

        //Получение списка ЧС по выбранному типу (по клику мыши)
        private void CheckType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmergID.Clear();
                //Составление и отпарвка запроса к БД
                string sql = "SELECT ID,EmergTypeName FROM EmergType WHERE EmergID = " + (lbType.SelectedIndex + 1);
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                List<string> list = new List<string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            EmergID.Add(reader.GetInt32(0));
                            list.Add(reader.GetString(1));
                        }
                    }
                }

                //Запись ответа в текстовые блоки
                List<TextBlock> tb = new List<TextBlock>();
                lbEmerg.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    tb.Add(new TextBlock
                    {
                        Text = list[i],
                        TextWrapping = TextWrapping.WrapWithOverflow
                    });
                    lbEmerg.Items.Add(tb[i]);
                }
                lbEmerg.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Получение списка ЧС по выбранному типу (по касанию экрана)
        private void CheckType_TouchUp(object sender, RoutedEventArgs e)
        {
            try
            {
                EmergID.Clear();
                //Составление и отпарвка запроса к БД
                string sql = "SELECT ID,EmergTypeName FROM EmergType WHERE EmergID = " + (lbType.SelectedIndex + 1);
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                List<string> list = new List<string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            EmergID.Add(reader.GetInt32(0));
                            list.Add(reader.GetString(1));
                        }
                    }
                }

                //Запись ответа в текстовые блоки
                List<TextBlock> tb = new List<TextBlock>();
                lbEmerg.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    tb.Add(new TextBlock
                    {
                        Text = list[i],
                        TextWrapping = TextWrapping.WrapWithOverflow
                    });
                    lbEmerg.Items.Add(tb[i]);
                }
                lbEmerg.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Переход на страницу выполнения работ (по клику мышки)
        private void CheckEmerg_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmerg.SelectedIndex != -1)
            {
                PageCSTasks pageCSTasks = new PageCSTasks(EmergID[lbEmerg.SelectedIndex]);
                NavigationService.Navigate(pageCSTasks);
            }
        }

        //Переход на страницу выполнения работ (по касанию экрана)
        private void CheckEmerg_TouchUp(object sender, RoutedEventArgs e)
        { 
            if (lbEmerg.SelectedIndex != -1)
            {
                PageCSTasks pageCSTasks = new PageCSTasks(EmergID[lbEmerg.SelectedIndex]);
                NavigationService.Navigate(pageCSTasks);
            }
        }
    }
}
