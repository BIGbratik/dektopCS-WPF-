using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Логика взаимодействия для PageAnalytics.xaml
    /// </summary>
    public partial class PageAnalytics : Page
    {
        desktopDBEntities1 db;
        string path = @".\data\Analityc\";
        public PageAnalytics()
        {
            InitializeComponent();
            db = new desktopDBEntities1();
            db.Analytic.Load();
            lb.ItemsSource = db.Analytic.Select(a => a.AnalyticFile).ToList();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void StartProgram_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex!=-1)
            {
                string fName = lb.SelectedValue.ToString();
                Process.Start($"{path}" + fName + ".exe");
            }

        }

        /*private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lb.Items.Add("Программа №" + (i + 1));
            }
        }*/
    }
}
