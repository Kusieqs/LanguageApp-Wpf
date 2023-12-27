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
            int column = Grid.GetColumn(sender as Button);
            int row = Grid.GetRow(sender as Button);
            string nameOfLanguage = MainGrid.Children.OfType<TextBlock>().Where(x => Grid.GetColumn(x) == column && Grid.GetRow(x) == row + 1).Select(x => x.Text).FirstOrDefault();
        }
        private void BtnAddLanguage(object sender, RoutedEventArgs e)
        {
            newLanguage = new NewLanguage();
            newLanguage.Owner = this;
            this.IsEnabled = false;
            newLanguage.Show();
            newLanguage.Closed += (s, args) => this.IsEnabled = true;
            newLanguage.Closed += (s, args) => this.Focus();
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
                TextBlock textBlock = new TextBlock();
                textBlock.Text = lan.ToString();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.FontSize = 20;
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.Foreground = Brushes.Black;
                Grid.SetColumn(textBlock, gridColumns);
                Grid.SetRow(textBlock, gridRows + 1);
                MainGrid.Children.Add(textBlock);

                Button button = new Button();
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/" + lan.Substring(0,3) + ".png");
                bitmap.EndInit();

                image.Source = bitmap;
                image.Stretch = Stretch.Fill;
                button.Content = image;
                button.Margin = new Thickness(10);
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                button.Foreground = Brushes.Transparent;
                button.IsEnabled = true;
                button.Click += BtnFlag;
                Grid.SetColumn(button, gridColumns);
                Grid.SetRow(button, gridRows);
                MainGrid.Children.Add(button);
                gridColumns++;
                if (gridColumns == 4)
                {
                    gridColumns = 0;
                    gridRows += 2;
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
                newLanguage.Focus();
            }
        } // Preventing from closing main window when new language window is open
    }
}
