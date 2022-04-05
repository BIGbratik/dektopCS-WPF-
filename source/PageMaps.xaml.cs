using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;

namespace dektopCS.source
{
    public partial class PageMaps : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];

        public PageMaps()
        {
            InitializeComponent();
        }

        //Возврат к предыдущей странице
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Метод перехода к списку имеющихся общих карт
        private void CheckMap_Click(object sender, RoutedEventArgs e)
        {
            PageMPSinfo pageMPSinfo = new PageMPSinfo("Maps");
            NavigationService.Navigate(pageMPSinfo);
        }

        //Метод перехода к списку имеющихся общих планов
        private void CheckPlans_Click(object sender, RoutedEventArgs e)
        {
            PageMPSinfo pageMPSinfo = new PageMPSinfo("Plans");
            NavigationService.Navigate(pageMPSinfo);
        }

        //Метод перехода к списку имеющихся общих схем
        private void CheckShemes_Click(object sender, RoutedEventArgs e)
        {
            PageMPSinfo pageMPSinfo = new PageMPSinfo("Shemes");
            NavigationService.Navigate(pageMPSinfo);
        }
    }
}
