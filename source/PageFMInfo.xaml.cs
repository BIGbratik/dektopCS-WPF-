using GMap.NET.WindowsPresentation;
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
        //Инициализация объекта ЧС
        PMobject pmObject=new PMobject();
        public PageFMInfo(PMobject obj)
        {
            InitializeComponent();
            pmObject = obj;
            Name.Text = pmObject.Name;
            Structer.Text = pmObject.Structer;
            Subord.Text = pmObject.Subord;
            isReady.Text = pmObject.isReady;
            Count.Text = pmObject.Count;
            Place.Text = pmObject.Place;
            Phone.Text = pmObject.Phone;
        }

        //Возврат на предыдущю страницу и удаление метки объекта с карты
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GMapControl map = (GMapControl)App.Current.MainWindow.FindName("mapView");
            App.Current.Resources["markerPath"] = "";
            map.Markers.Clear();
        }

        //Переход на страницу дополнительных графических материалов, относящихс к данному объекту
        private void MPS_Click(object sender, RoutedEventArgs e)
        {
            PageMPS pageMPS = new PageMPS(pmObject.Number);
            NavigationService.Navigate(pageMPS);
        }
    }
}
