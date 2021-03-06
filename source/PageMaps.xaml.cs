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
    /// Логика взаимодействия для PageMaps.xaml
    /// </summary>
    public partial class PageMaps : Page
    {
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
