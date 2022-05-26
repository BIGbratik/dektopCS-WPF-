using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace dektopCS.source
{
    public partial class PageCSAdd : Page
    {
        //Получение данных подключения к БД
        private readonly MySqlConnection myConnection = (MySqlConnection)App.Current.Resources["connectionMySQL"];

        private readonly int EmergTypeID;
        private readonly List<FileInfo> docs;
        private readonly string dir;
        public PageCSAdd(int id)
        {
            InitializeComponent();
            EmergTypeID = id;
            dir = Directory.GetCurrentDirectory() + @"\data\Documentation\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            docs = new List<FileInfo>();
        }

        //Метод заполнения страницы при загрузке
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Составление и отпарвка запроса к БД
                string sql = "SELECT EmergTypeName FROM EmergType WHERE ID = " + EmergTypeID;
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);

                //Чтение ответа БД построчно
                string str = "";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //Построчное считывание ответа
                        while (reader.Read())
                        {
                            str = reader.GetString(0);
                        }
                    }
                }
                CSType.Content = str;
                //CSDate.Content = (DateTime.Now).ToString("G");
                CSDate.Text = (DateTime.Now).ToString("G");
            }
            catch
            {
                MessageBox.Show("Не удалось выгрузить данные из базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Запоминание выбранного файла
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                FilterIndex = 2,
                Filter = "Документ (*.doc)|*.doc|Документ (*.docx)|*.docx|Изображение (*.jpg)|*.jpg|Изображение (*.png)|*.png"
            };
            if (dlg.ShowDialog() == true)
            {
                var filePath = dlg.FileName;
                FileInfo file = new FileInfo(filePath);
                docs.Add(file);
                TextBlock tb = new TextBlock()
                {
                    Text = file.Name
                };
                lf.Items.Add(tb);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if ((CSName.Text!="")&&(CSParams.Text!="")&&(CSMeasurs.Text!=""))
            {            
                try
                {   
                    string sql = "INSERT EmergTasks(EmergTypeID, TaskName, EmergTime, EmergParams, EmergMeasures) VALUE (" 
                        + EmergTypeID+",'"+ CSName.Text + "',@dt,'"+CSParams.Text+"','"+CSMeasurs.Text+"')";
                    ///
                    ///Запрос на добавление файлов!!!
                    ///
                    foreach (FileInfo file in docs)
                    {
                        switch (file.Extension)
                        {
                            case ".doc":
                                file.CopyTo(dir + file.Name, true);
                                break;
                            case ".docx":
                                file.CopyTo(dir + file.Name, true);
                                break;
                            case ".jpg":
                                file.CopyTo(dir + file.Name, true);
                                break;
                            case ".png":
                                file.CopyTo(dir + file.Name, true);
                                break;
                        }
                    }
                    
                    MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                    cmd.Parameters.AddWithValue("@dt", DateTime.Parse(CSDate.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Сохранение прошло успешно","Успех!!!",MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения","Внимание!!!",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы заполнили не все поля!", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
