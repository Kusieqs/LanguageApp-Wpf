using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data.Sql;
using System.ComponentModel;

namespace LanguageAppWpf
{
    public partial class LanguageChoose : Window
    {
        private NewLanguage newLanguage;
        private ChoosingUnit choosingUnit;
        public LanguageChoose()
        {
            InitializeComponent();
            this.Loaded += ActivData;
            this.Activated += ActivData;
        }
        private void ActivData(object sender, EventArgs e) // Adding flags as buttons when window is loaded or activated 
        {
            AddingFlagsAsButtons();
        }
        private void BtnFlag(object sender, RoutedEventArgs e)
        {
            string nameOfLanguage = (sender as Button).Name;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/" + nameOfLanguage.Substring(0, 3) + ".png"));
            image.Margin = new Thickness(20, 10, 20, 10);
            Grid.SetColumn(image, 1);
            Grid.SetRow(image, 0);
            UIElement elementRemoveBtn = MainGrid.Children.Cast<UIElement>().FirstOrDefault(x => Grid.GetColumn(x) == 1 && Grid.GetRow(x) == 0);
            if (elementRemoveBtn != null)
            {
                MainGrid.Children.Remove(elementRemoveBtn);
            }
            MainGrid.Children.Add(image);
            // dokonczyc
            
        }
        private void BtnAddLanguage(object sender, RoutedEventArgs e)
        {
            newLanguage = new NewLanguage();
            newLanguage.Owner = this;
            newLanguage.Show();
            this.IsEnabled = false;
            newLanguage.Closed += (s, args) => this.IsEnabled = true;
        } // Creating new window for adding new language
        private void AddingFlagsAsButtons()
        {
            int gridRows = 1;
            int gridColumns = 0;
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = System.IO.Path.Combine(appDataFolder, "LanguageAppWpf");
            List<string> languages = new List<string>();
            ExistingFolder(path, ref languages);

            foreach (string lan in languages)
            {
                Button button = new Button();
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/" + lan.Substring(0,3) + ".png");
                bitmap.EndInit();

                image.Source = bitmap;
                image.Stretch = Stretch.Fill;
                button.Content = image;
                button.Margin = new Thickness(20,10,20,10);
                button.Name = lan;
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                button.Foreground = Brushes.Transparent;
                button.IsEnabled = true;
                button.Click += BtnFlag;
                Grid.SetColumn(button, gridColumns);
                Grid.SetRow(button, gridRows);
                UIElement elementRemoveBtn = BorderLan.Children.Cast<UIElement>().FirstOrDefault(x => Grid.GetColumn(x) == gridColumns && Grid.GetRow(x) == gridRows);
                if (elementRemoveBtn != null)
                {
                    BorderLan.Children.Remove(elementRemoveBtn);
                }
                BorderLan.Children.Add(button);
                gridColumns++;
                if (gridColumns == 2)
                {
                    gridColumns = 0;
                    gridRows++;
                }
            }
        } // Adding flags as buttons
        private void ExistingFolder(string path, ref List<string> languages)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(System.IO.Path.Combine(path, NameOfLanguage.English.ToString()));
            }
            languages = new List<string>(Directory.GetDirectories(path).Select(System.IO.Path.GetFileName).ToList());
        } // Checking if folder exists
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (newLanguage != null && newLanguage.IsVisible)
            {
                e.Cancel = true;
            }
            if (choosingUnit != null && choosingUnit.IsVisible)
            {
                e.Cancel = true;
            }
        } // Preventing from closing main window when new language window is open

        private void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnContinue(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNewUnit(object sender, RoutedEventArgs e)
        {

        }
    }
}
