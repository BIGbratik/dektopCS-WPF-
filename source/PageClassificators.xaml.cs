using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageClassificators.xaml
    /// </summary>
    public partial class PageClassificators : Page
    {
        //Инициализации БД и объекта будужщей страницы
        desktopDBEntities1 db;
        object page;
        public PageClassificators()
        {
            //Инициализация страницы с выгрузкой необходимых данных из БД
            InitializeComponent();
            db = new desktopDBEntities1();
            string left = db.Emerg.Where(a => a.ID.Equals(1)).Select(b => b.EmergName).FirstOrDefault();
            BtnLeft.Content = left;
            string right = db.Emerg.Where(a => a.ID.Equals(2)).Select(b => b.EmergName).FirstOrDefault();
            BtnRight.Content = right;
            page = Claasifs.Content;
        }

        //Возвращение на предыдущую страницу
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Просмотр информации по перовму типу ЧС
        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            ShowTypes(1);
        }

        //Просмотр информации по второму типу ЧС
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            ShowTypes(2);
        }

        //Метод отрисовки страницы в зависимости от выбранного характера ЧС
        private void ShowTypes (int id)
        {
            List<string> types = new List<string>();
            db = new desktopDBEntities1();
            types = db.EmergType.Where(a => a.EmergID.Equals(id)).Select(b => b.EmergTypeName).ToList();

            Grid grid = new Grid();
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            row1.Height = new GridLength(40);
            row3.Height = new GridLength(40);

            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);

            TextBlock tb = new TextBlock();
            tb.Text = "Виды ЧС";
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.FontSize = 20;
            tb.Foreground = Brushes.White;

            Button returnBtn = new Button();
            returnBtn.Content = "Вернуться к выбору характера ЧС";
            returnBtn.Click += ReturnBtn_Click;

            ListBox lb = new ListBox
            {
                Name = "lb",
                ItemsSource = types,
            };

            grid.Children.Add(returnBtn);
            grid.Children.Add(tb);
            grid.Children.Add(lb);
            
            Grid.SetRow(tb, 0);
            Grid.SetRow(lb, 1);
            Grid.SetRow(returnBtn, 2);
            
            Claasifs.Content = grid;
        }

        //Метод отрисовки стартового состояния окна
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            Claasifs.Content = page;
        }

        private void LB_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
