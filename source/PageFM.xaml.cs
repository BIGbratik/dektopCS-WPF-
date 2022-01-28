﻿using System;
using System.Collections.Generic;
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

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageFM.xaml
    /// </summary>
    public partial class PageFM : Page
    {
        public static int ind;
        public List<PMobject> objs = new List<PMobject>();
        public PageFM(int num)
        {
            InitializeComponent();
            ind = num;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void CheckInfo_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex!=-1)
            {
                PageFMInfo pageFMInfo = new PageFMInfo(objs[lb.SelectedIndex]);
                NavigationService.Navigate(pageFMInfo);
            }
        }

        private void Objs_Loaded(object sender, RoutedEventArgs e)
        {
            lb.Items.Clear();
            string path = @"./data/FM";
            string[] str;
            using (FileStream fStr = new FileStream($"{path}/FM" + ind + ".txt", FileMode.OpenOrCreate))
            {
                byte[] arr = new byte[fStr.Length];
                fStr.Read(arr, 0, arr.Length);
                string fText = System.Text.Encoding.UTF8.GetString(arr);
                str = fText.Split('\n');
            }

            for (int i = 0; i < str.Length; i++)
            {
                string[] line = str[i].Split('|');
                try
                {
                    PMobject obj = new PMobject();
                    obj.Number = Int32.Parse(line[0]);
                    obj.Name = line[1];
                    obj.Structer = line[2];
                    obj.Subord = line[3];
                    obj.isReady = line[4];
                    obj.Count = line[5];
                    obj.Place = line[6];
                    obj.Phone = line[7];

                    string mes = obj.Number.ToString() + ") ";
                    if (obj.Name.Equals(" "))
                    {
                        mes += "наименование объекта неизвестно";
                    }
                    else
                    {
                        mes += obj.Name;
                    }
                    if (obj.Structer.Equals(" "))
                    {
                        mes += " (ведомственная принадлежность неизвестна)";
                    }
                    else
                    {
                        mes += " (" + obj.Structer + ")";
                    }
                    lb.Items.Add(mes);
                    objs.Add(obj);
                }
                catch (FormatException) { }
            }
        }
    }
}