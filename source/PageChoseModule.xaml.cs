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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageChoseModule.xaml
    /// </summary>
    public partial class PageChoseModule : Page
    {
        public PageChoseModule()
        {
            InitializeComponent();
        }

        private void CheckForcMeans_Click(object sender, RoutedEventArgs e)
        {
            PageForceMeans pageForceMeans = new PageForceMeans();
            NavigationService.Navigate(pageForceMeans);
        }
        private void CheckMaps_Click(object sender, RoutedEventArgs e)
        {
            WindowMaps windowMaps = new WindowMaps();
            windowMaps.Show();
        }
        private void CheckAnalytics_Click(object sender, RoutedEventArgs e)
        {
            WindowAnalytics windowAnalytics = new WindowAnalytics();
            windowAnalytics.Show();
        }
        private void CheckGuids_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль СПРАВОЧНИКИ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckMultimedia_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль МУЛЬТИМЕДИА находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
