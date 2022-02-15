using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для PageMPSinfo.xaml
    /// </summary>
    public partial class PageMPSinfo : Page
    {
        desktopDBEntities1 db;
        private string path = @"/data/MPS/";
        public PageMPSinfo(string type)
        {
            InitializeComponent();
            path =path+type+"/";
            db = new desktopDBEntities1();
            db.MPS.Load();

            var list = db.MPS.Where(x => x.ObjectID.HasValue.Equals(false)).Where(y=>y.MPSType.Equals(type)).Select(a => a.MPSfile).ToList();
            if (list.Count == 0)
            {
                lb.Items.Add("В базе данных отсутствуют объекты, связанные с выбранным объектом");
                lb.IsEnabled = false;
            }
            else
            {
                lb.ItemsSource = list;
                lb.IsEnabled = true;
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                string fName=lb.SelectedItem.ToString();
                Image image = new Image();
                Uri uri = new Uri(path+fName, UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                img.Source = bitmapImage;

                lb.SelectedIndex = -1;
            }
        }
    }
}

