﻿using System;
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

namespace LanguageAppWpf
{
    public partial class LanguageChoose : Window
    {
        public LanguageChoose()
        {
            InitializeComponent();
            this.Loaded += ActivData;
            this.Activated += ActivData;
        }
        private void ActivData(object sender, EventArgs e)
        {
            AddingFlagsAsButtons();
        } 
        private void BtnAddLanguage(object sender, RoutedEventArgs e)
        {
            NewLanguage newLanguage = new NewLanguage();
            this.IsEnabled = false;
            newLanguage.Show();
            newLanguage.Closed += (s, args) => this.IsEnabled = true;
        }
        
        private void AddingFlagsAsButtons()
        {
            int gridRows = 1;
            int gridColumns = 0;
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = System.IO.Path.Combine(appDataFolder, "LanguageAppWpf");
            List<Language> languages = new List<Language>();
            ExistingFolder(path, ref languages);

            foreach (Language lan in languages)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = lan.nameOfLanguage.ToString();
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
                bitmap.UriSource = new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/" + lan.abbreviation + ".png");
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
        }
        private void ExistingFolder(string path, ref List<Language> languages)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Language english = new Language(NameOfLanguage.English);
                languages.Add(english);
                string jsonCreator = JsonConvert.SerializeObject(languages);
                File.WriteAllText(System.IO.Path.Combine(path, "Languages"), jsonCreator);
                Directory.CreateDirectory(System.IO.Path.Combine(path, NameOfLanguage.English.ToString()));
            }
            else
            {
                string json = File.ReadAllText(System.IO.Path.Combine(path, "Languages"));
                languages = JsonConvert.DeserializeObject<List<Language>>(json);
            }
        }
        private void BtnFlag(object sender, RoutedEventArgs e)
        {
        }
    }
}
