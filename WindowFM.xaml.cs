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
            MessageBox.Show("\nНаименование - " + objs[lb.SelectedIndex].Name + 
                "\nВедомственная принадлежность - " + objs[lb.SelectedIndex].Structer +
                "\nПодчиненность (федеральная, региональная, территориальная, местная, объектовая) - "+ objs[lb.SelectedIndex].Subord +
                "\nСтепень готовности (постоянная, повышенная, общая) - " + objs[lb.SelectedIndex].isReady +
                "\nЧисленный состав  - " + objs[lb.SelectedIndex].Count +
                "\nМесто дислокации (адрес) - " + objs[lb.SelectedIndex].Place +
                "\nТелефон - " + objs[lb.SelectedIndex].Phone);
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
                    PMobject obj = new PMobject(Int32.Parse(line[0]), line[1]);
                    lb.Items.Add(obj.Number + ") " + obj.Name);
                    obj.Structer = line[2];
                    obj.Subord = line[3];
                    obj.isReady = line[4];
                    obj.Count = Int32.Parse(line[5]);
                    obj.Place = line[6];
                    obj.Phone = line[7];
                    objs.Add(obj);
                }
                catch (FormatException){}
            }
        }
    }
}
