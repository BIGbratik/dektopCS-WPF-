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
    /// Логика взаимодействия для PageFMInfo.xaml
    /// </summary>
    public partial class PageFMInfo : Page
    {
        public PageFMInfo(PMobject obj)
        {
            InitializeComponent();
            Name.Text = obj.Name;
            Structer.Text = obj.Structer;
            Subord.Text = obj.Subord;
            isReady.Text = obj.isReady;
            Count.Text = obj.Count;
            Place.Text = obj.Place;
            Phone.Text = obj.Phone;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /*private void PageInfo_Loaded(object sender, RoutedEventArgs e)

        {
            Name.Text = obj.Name;
            Structer.Text = obj.Structer;
            Subord.Text = obj.Subord;
            isReady.Text = obj.isReady;
            Count.Text = obj.Count;
            Place.Text = obj.Place;
            Phone.Text = obj.Phone;
        }*/
    }
}
