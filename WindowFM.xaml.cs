using System;
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
using System.Windows.Shapes;

namespace dektopCS
{
    /// <summary>
    /// Логика взаимодействия для WindowFM.xaml
    /// </summary>
    public partial class WindowFM : Window
    {
        public static int ind;
        public List<PMobject> objs = new List<PMobject>();
        public WindowFM(int num)
        {
            InitializeComponent();
            ind = num;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CheckInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowFMInfo fmInfo= new WindowFMInfo(objs[lb.SelectedIndex]);
            fmInfo.Show();
        }

        private void Objs_Loaded(object sender, RoutedEventArgs e)
        {

            string path = @"./data/FM";
            string[] str;
            using (FileStream fStr = new FileStream($"{path}/FM" +ind+ ".txt", FileMode.OpenOrCreate))
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
                catch (FormatException){}
            }
        }
    }
}
