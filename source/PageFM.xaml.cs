using GMap.NET;
using GMap.NET.WindowsPresentation;
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
    /// Логика взаимодействия для PageFM.xaml
    /// </summary>
    public partial class PageFM : Page
    {
        //Инициализация переменной индекса, объекта ЧС и БД
        public static int ind;
        PMobject obj = new PMobject();
        desktopDBEntities1 db;
        public PageFM(int num)
        {
            //Инициализация страницы с выгрузкой необходимых данных из БД
            InitializeComponent();
            ind = num;
            db = new desktopDBEntities1();
            db.CSobject.Load();
            lb.ItemsSource = db.CSobject.Where(p => p.CategoryID.Equals(num)).Select(a => a.ObjectName).ToList();
        }

        //Возвращение на предыдущую страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Переход к странице подробной информации об выбранном объекте СиС и постановка на карте метки выбранного объекта
        private void CheckInfo_Click(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                int selectedObjectID = Convert.ToInt32(db.CSobject.Where(p => p.CategoryID.Equals(ind)).Select(a => a.ID).FirstOrDefault()) + lb.SelectedIndex;

                obj.Number = selectedObjectID;
                obj.Name = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.ObjectName).FirstOrDefault();
                obj.Structer = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Vedomstvo).FirstOrDefault();
                obj.Subord = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Subordination).FirstOrDefault();
                obj.isReady = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.IsReady).FirstOrDefault();
                obj.Count = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Num).FirstOrDefault();
                obj.Place = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Place).FirstOrDefault();
                obj.Phone = db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Phone).FirstOrDefault();
                PageFMInfo pageFMInfo = new PageFMInfo(obj);
                NavigationService.Navigate(pageFMInfo);

                try
                {
                    obj.Latitude = (double)db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Latitude).FirstOrDefault();
                    obj.Longitude = (double)db.CSobject.Where(a => a.ID.Equals(selectedObjectID)).Select(a => a.Longitude).FirstOrDefault();

                    GMapControl map = (GMapControl)App.Current.MainWindow.FindName("mapView");
                    App.Current.Resources["markerPath"] = "";

                    GMapMarker marker = new GMapMarker(new PointLatLng(obj.Latitude, obj.Longitude));
                        Ellipse img = new Ellipse()
                        {
                            Width = 20,
                            Height = 20,
                            ToolTip = obj.Name,
                            Fill = Brushes.MidnightBlue,
                            Stroke = Brushes.DarkOrange,
                            StrokeThickness = 3
                        };

                        marker.Shape = img;
                        map.Markers.Add(marker);
                }
                catch (Exception ex)
                { }
                
            }
        }
    }
}
