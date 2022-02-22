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
    /// Логика взаимодействия для PageMPS.xaml
    /// </summary>
    public partial class PageMPS : Page
    {
        //Инициализация БД, пути к локальным данным, списка для нужных данных
        desktopDBEntities1 db;
        private string path = @"/data/MPS/";
        List<string> types = new List<string>();
        public PageMPS(int id)
        {
            //Инициализация страницы и заполнение её выбранным перечнем данных
            InitializeComponent();
            db = new desktopDBEntities1();
            db.MPS.Load();
            List<string> str = new List<string>();


            var list = db.MPS.Where(x => x.ObjectID.HasValue.Equals(true)).Select(a => new {a.ObjectID, a.MPSfile, a.MPSType}).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ObjectID == id)
                {
                    str.Add(list[i].MPSfile);
                    types.Add(list[i].MPSType);
                }
            }
            if (str.Count==0)
            {
                lb.Items.Add("В базе данных отсутствуют объекты, связанные с выбранным объектом");
                lb.IsEnabled = false;
            }
            else
            {
                lb.ItemsSource = str;
                lb.IsEnabled = true;
            }
        }

        //Возврат на предыдущю страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Отрисовка выбранного изображения
        private void CheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                path += types[lb.SelectedIndex] + "/"+ lb.SelectedItem.ToString();
                Image image = new Image();
                Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
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
