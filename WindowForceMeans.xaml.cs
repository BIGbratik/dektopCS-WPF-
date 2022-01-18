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
            MessageBox.Show(lb.SelectedItem.ToString());
        }

        private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i=0; i<10;i++)
                lb.Items.Add("Объект "+i);
        }
    }

    class CSobject
    {

    }

}
