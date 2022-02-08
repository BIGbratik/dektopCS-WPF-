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

namespace dektopCS.source
{
    /// <summary>
    /// Логика взаимодействия для PageMultimedia.xaml
    /// </summary>
    public partial class PageMultimedia : Page
    {
        desktopDBEntities1 db;
        public PageMultimedia()
        {
            InitializeComponent();

            List<string> fNames = new List<string>();
            List<string>marksNames=new List<string>();
            db = new desktopDBEntities1();
            db.Marks.Load();
            fNames = db.Marks.Select(a => a.MakrImage).ToList();
            marksNames= db.Marks.Select(a => a.MarkName).ToList();
            lb.ItemsSource = GetSP(fNames,marksNames);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChooseItem_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                db = new desktopDBEntities1();
                db.Marks.Load();
                string markerPath = db.Marks.Where(a=>a.ID.Equals(lb.SelectedIndex+1)).Select(b=>b.MakrImage).FirstOrDefault();
                App.Current.Resources["markerPath"] = markerPath;
            }
        }

        public List<StackPanel> GetSP(List<string> fn, List<string>mn)
        {
            string path = "/data/Marks/";
            List<StackPanel> stackPanel = new List<StackPanel>();
            for (int i = 0; i < fn.Count; i++)
            {
                string img = path + fn[i];

                TextBlock tb = new TextBlock
                {
                    Text = mn[i]
                };
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.FontSize = 16;

                Image image = new Image();
                Uri uri = new Uri(img,   UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = uri;
                bitmapImage.EndInit();

                image.Source = bitmapImage;
                image.Margin = new Thickness(5);

                StackPanel sp = new StackPanel();

                Grid grid = new Grid();

                ColumnDefinition column1 = new ColumnDefinition();
                column1.Width = new GridLength(60);

                ColumnDefinition column2 = new ColumnDefinition();
                
                RowDefinition row1= new RowDefinition();
                row1.Height = new GridLength(60);

                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                grid.RowDefinitions.Add(row1);

                grid.Children.Add(image);
                grid.Children.Add(tb);

                Grid.SetRow(image, 0);
                Grid.SetRow(tb, 0);
                Grid.SetColumn(image, 0);
                Grid.SetColumn(tb, 1);

                sp.Children.Add(grid);
                sp.Orientation=Orientation.Horizontal;

                stackPanel.Add(sp);
            }
            return stackPanel;
        }
    }
}
