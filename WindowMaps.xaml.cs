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
    /// Логика взаимодействия для WindowMaps.xaml
    /// </summary>
    public partial class WindowMaps : Window
    {
        public WindowMaps()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckMap_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль КАРТЫ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckPlans_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ПЛАНЫ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckShemes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль СХЕМЫ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
