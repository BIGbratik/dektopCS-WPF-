using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace dektopCS.source
{
    public partial class PageFMInfo : Page
    {
        //Инициализация объекта ЧС
        private readonly PMobject obj=new PMobject();
        public PageFMInfo(PMobject obj)
        {
            InitializeComponent();
            this.obj = obj;
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = obj.Name;
            Structer.Text = obj.Structer;
            Subord.Text = obj.Subord;
            isReady.Text = obj.isReady;
            Count.Text = obj.Count;
            Place.Text = obj.Place;
            Phone.Text = obj.Phone;
        }

        //Возврат на предыдущю страницу и удаление метки объекта с карты
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GMapControl map = (GMapControl)App.Current.Windows[1].FindName("mapView");
            App.Current.Resources["markerPath"] = "";
            map.Markers.Clear();
            map.Zoom = 10;
        }

        //Переход на страницу дополнительных графических материалов, относящихс к данному объекту
        private void MPS_Click(object sender, RoutedEventArgs e)
        {
            PageMPS pageMPS = new PageMPS(obj.Number);
            NavigationService.Navigate(pageMPS);
        }
    }
}
