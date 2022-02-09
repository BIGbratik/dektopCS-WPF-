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
        public static int ind;
        PMobject obj = new PMobject();
        desktopDBEntities1 db;
        public PageFM(int num)
        {
            InitializeComponent();
            ind = num;
            db = new desktopDBEntities1();
            db.CSobject.Load();
            lb.ItemsSource = db.CSobject.Where(p => p.CategoryID.Equals(num)).Select(a => a.ObjectName).ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
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

        /*private void Objs_Loaded(object sender, RoutedEventArgs e)
        {
            /*lb.Items.Clear();
            string path = @"./data/FM";
            string[] str;
            using (FileStream fStr = new FileStream($"{path}/FM" + ind + ".txt", FileMode.OpenOrCreate))
            {
                byte[] arr = new byte[fStr.Length];
                fStr.Read(arr, 0, arr.Length);
                string fText = System.Text.Encoding.UTF8.GetString(arr);
                str = fText.Split('\n');
            }

            for (int i = 0; i < str.Length; i++)
            {
                string[] line = str[i].Split('|');
                try
                {
                    PMobject obj = new PMobject();
                    obj.Number = Int32.Parse(line[0]);
                    obj.Name = line[1];
                    obj.Structer = line[2];
                    obj.Subord = line[3];
                    obj.isReady = line[4];
                    obj.Count = line[5];
                    obj.Place = line[6];
                    obj.Phone = line[7];

                    string mes = obj.Number.ToString() + ") ";
                    if (obj.Name.Equals(" "))
                    {
                        mes += "наименование объекта неизвестно";
                    }
                    else
                    {
                        mes += obj.Name;
                    }
                    if (obj.Structer.Equals(" "))
                    {
                        mes += " (ведомственная принадлежность неизвестна)";
                    }
                    else
                    {
                        mes += " (" + obj.Structer + ")";
                    }
                    lb.Items.Add(mes);
                    objs.Add(obj);
                }
                catch (FormatException) { }
            }
        }*/
    }
}
