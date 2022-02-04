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
        private void CheckPassports_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ПАСПОРТА БЕЗОПАСНОСТИ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void CheckIS_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ИНФОРМАЦИОННАЯ БЕЗОПАСНОСТЬ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckNorSprav_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль НОРМАТИВНО-СПРАВОЧНАЯ ИНФОРМАЦИЯ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckKlassifs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль КЛАССИФИКАТОРЫ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckExtSys_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ВНЕШНИЕ СИСТЕМЫ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckInfExchange_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ИНФОРМАЦИОННЫЙ ОБМЕН находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckGuids_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль СПРАВОЧНИКИ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CheckMultimedia_Click(object sender, RoutedEventArgs e)
        {
            PageMultimedia pageMultimedia = new PageMultimedia();
            NavigationService.Navigate(pageMultimedia);
        }
    }
}
