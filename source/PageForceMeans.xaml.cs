﻿using System;
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

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageForceMeans.xaml
    /// </summary>
    public partial class PageForceMeans : Page
    {
        desktopDBEntities1 db;
        public PageForceMeans()
        {
            InitializeComponent();
            db = new desktopDBEntities1();
            db.Category.Load();
            lb.ItemsSource = db.Category.Select(a => a.CategoryName).ToList();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex!=-1)
            {
                PageFM pageFM = new PageFM(lb.SelectedIndex + 1);
                NavigationService.Navigate(pageFM);
                lb.SelectedIndex = -1;
            }
        }

        /*private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            lb.Items.Clear();
            string path = @"./data";
            string[] str;
            using (FileStream fStr = new FileStream($"{path}/" + "/ForceMeans.txt", FileMode.OpenOrCreate))
            {
                byte[] arr = new byte[fStr.Length];
                fStr.Read(arr, 0, arr.Length);
                string fText = System.Text.Encoding.UTF8.GetString(arr);
                str = fText.Split('\n');
            }

            for (int i = 0; i < str.Length; i++)
            {
                TextBlock tb = new TextBlock
                {
                    TextWrapping = TextWrapping.WrapWithOverflow
                };
                string[] line = str[i].Split('|');
                string[] kostil = line[1].Split('\r');

                tb.Text = line[0] + ") " + kostil[0];
                lb.Items.Add(tb);
            }
        }*/
    }
}
