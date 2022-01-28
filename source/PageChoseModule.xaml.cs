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
            PageMaps pageMaps = new PageMaps();
            NavigationService.Navigate(pageMaps);
        }
        private void CheckAnalytics_Click(object sender, RoutedEventArgs e)
        {
            PageAnalytics pageAnalytics = new PageAnalytics();
            NavigationService.Navigate(pageAnalytics);
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
