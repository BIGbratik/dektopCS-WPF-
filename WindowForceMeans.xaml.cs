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
    /// Логика взаимодействия для WindowForceMeans.xaml
    /// </summary>
    public partial class WindowForceMeans : Window
    {
        public WindowForceMeans()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            WindowFM windowFM = new WindowFM(lb.SelectedIndex+1);
            windowFM.Show();
        }

        private void Item_Loaded(object sender, RoutedEventArgs e)
        {

            string path = @"./data";
            string[] str;
            using (FileStream fStr = new FileStream($"{path}/" + "ForceMeans.txt", FileMode.OpenOrCreate))
            {
                byte[] arr = new byte[fStr.Length];
                fStr.Read(arr, 0, arr.Length);
                string fText = System.Text.Encoding.UTF8.GetString(arr);
                str = fText.Split('\n');
            }

            for (int i = 0; i < str.Length; i++)
            {
                string[] line = str[i].Split('|');
                lb.Items.Add(line[0] + ") " + line[1]);
            }
        }
    }
}
