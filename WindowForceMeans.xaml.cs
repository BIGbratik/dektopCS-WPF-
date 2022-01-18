using System;
using System.Collections.Generic;
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
        public List<CSobject> cSobjects = new List<CSobject>();
        public WindowForceMeans()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Item_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\nНазвание объекта: " + cSobjects[lb.SelectedIndex].Name + "\nОписание: " + cSobjects[lb.SelectedIndex].Description);
        }

        private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i=0; i<5;i++)
            {
                CSobject csObj = new CSobject(i,(i+1).ToString(),"Некоторое описание");
                cSobjects.Add(csObj);
                lb.Items.Add(cSobjects[i].Name);
            }
        }
    }

    public class CSobject
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CSobject(int num, string name, string str)
        {
            this.Number = num;
            this.Name = name;
            this.Description = str;
        }
    }

}
