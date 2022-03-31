using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace dektopCS.source
{
    public partial class PageChoseModule : Page
    {
        public PageChoseModule()
        {
            InitializeComponent();
        }

        //Метод перехода на страницу сил и средств
        private void CheckForcMeans_Click(object sender, RoutedEventArgs e)
        {
            PageForceMeans pageForceMeans = new PageForceMeans();
            NavigationService.Navigate(pageForceMeans);
        }

        //Метод перехода на страницу паспортов безопасности
        private void CheckPassports_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ПАСПОРТА БЕЗОПАСНОСТИ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Метод перехода на страницу карт, планов и схем
        private void CheckMaps_Click(object sender, RoutedEventArgs e)
        {
            PageMaps pageMaps = new PageMaps();
            NavigationService.Navigate(pageMaps);
        }

        //Метод перехода на страницу выбора аналитической программы
        private void CheckAnalytics_Click(object sender, RoutedEventArgs e)
        {
            PageAnalytics pageAnalytics = new PageAnalytics();
            NavigationService.Navigate(pageAnalytics);
        }

        //Метод перехода на страницу модуля информационной безопасности
        private void CheckIS_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ИНФОРМАЦИОННАЯ БЕЗОПАСНОСТЬ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Метод перехода на страницу модуля нормативно-справочной информации
        private void CheckNorSprav_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль НОРМАТИВНО-СПРАВОЧНАЯ ИНФОРМАЦИЯ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Метод перехода на страницу классификаторов
        private void CheckClassifs_Click(object sender, RoutedEventArgs e)
        {
            PageClassificators pageClassificators = new PageClassificators();
            NavigationService.Navigate(pageClassificators);
        }

        //Метод перехода на страницу внешних систем
        private void CheckExtSys_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ВНЕШНИЕ СИСТЕМЫ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Метод перехода на страницу информационного обмена
        private void CheckInfExchange_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль ИНФОРМАЦИОННЫЙ ОБМЕН находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Метод перехода на страницу спровичников
        private void CheckGuids_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Модуль СПРАВОЧНИКИ находится в разработке", "ВНИМАНИЕ!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Метод перехода на сраницу мультимедии (значков для карты)
        private void CheckMultimedia_Click(object sender, RoutedEventArgs e)
        {
            PageMultimedia pageMultimedia = new PageMultimedia();
            NavigationService.Navigate(pageMultimedia);
        }

        //Метод перехода на сраницу просмотра действий при ЧС
        private void CheckCSAction_Click(object sender, RoutedEventArgs e)
        {
            PageCSAction pageCSAction= new PageCSAction();
            NavigationService.Navigate(pageCSAction);
        }
    }
}
