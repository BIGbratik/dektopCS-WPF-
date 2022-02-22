using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageForceMeans.xaml
    /// </summary>
    public partial class PageForceMeans : Page
    {
        //Инициализация БД
        desktopDBEntities1 db;
        public PageForceMeans()
        {
            //Инициализация страницы и выгрузка необходимых данных из БД
            InitializeComponent();
            db = new desktopDBEntities1();
            db.Category.Load();
            lb.ItemsSource = db.Category.Select(a => a.CategoryName).ToList();
        }

        //Возврат на предыдущю страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Переход на страницу объектов СиС из выбранной категории
        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex!=-1)
            {
                PageFM pageFM = new PageFM(lb.SelectedIndex + 1);
                NavigationService.Navigate(pageFM);
                lb.SelectedIndex = -1;
            }
        }
    }
}
