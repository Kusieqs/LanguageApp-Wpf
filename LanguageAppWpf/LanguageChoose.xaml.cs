﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace LanguageAppWpf
{
    public partial class LanguageChoose : Window
    {
        private NewLanguage newLanguage; // Creating new window for adding new language
        private NewUnit newUnit; // Creating new window for adding new unit
        private string actualLanguage; // Actual language
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
            deleteLanguageBtn.IsEnabled = true;
            actualLanguage = (sender as Button).Name;
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/" + actualLanguage.Substring(0, 3) + ".png")),
                Margin = new Thickness(20, 10, 20, 10),
                Stretch = Stretch.Fill  
            };
            Grid.SetColumn(image, 1);
            Grid.SetRow(image, 0);
            UIElement elementRemoveBtn = FlagGrid.Children.Cast<UIElement>().FirstOrDefault(x => Grid.GetColumn(x) == 1 && Grid.GetRow(x) == 0);
            
            if (elementRemoveBtn != null)
            {
                FlagGrid.Children.Remove(elementRemoveBtn);
            }

            FlagGrid.Children.Add(image);
            ReadComboBox(sender, e);
            
        }// Choosing language and adding flag to the grid
        private void BtnAddLanguage(object sender, RoutedEventArgs e)
        {
            newLanguage = new NewLanguage()
            {
                Owner = this,
            };
            newLanguage.Show();
            this.IsEnabled = false;
            newLanguage.Closed += (s, args) =>
            {
                IsEnabled = true;
                AddingFlagsAsButtons();
                Focus();
            };
        } // Creating new window for adding new language
        private void AddingFlagsAsButtons()
        {
            BorderLan.Children.Clear();
            int gridRows = 0, gridColumns = 0;
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            List<string> languages = new List<string>();

            ExistingFolder(Path.Combine(appDataFolder, "LanguageAppWpf"), ref languages);

            foreach (string lan in languages)
            {
                #region Adding flags as buttons
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/" + lan.Substring(0, 3) + ".png");
                bitmap.EndInit();

                Image image = new Image()
                {
                    Source = bitmap,
                    Stretch = Stretch.Fill
                };

                Button button = new Button()
                {
                    Content = image,
                    Margin = new Thickness(20, 10, 20, 10),
                    Name = lan,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                    Foreground = Brushes.Transparent,
                    IsEnabled = true
                };
                button.Click += BtnFlag;
                #endregion Adding flags as buttons
                
                Grid.SetColumn(button, gridColumns);
                Grid.SetRow(button, gridRows);
                UIElement elementRemoveBtn = BorderLan.Children.Cast<UIElement>().FirstOrDefault(x => Grid.GetColumn(x) == gridColumns && Grid.GetRow(x) == gridRows);

                if (elementRemoveBtn != null)
                    BorderLan.Children.Remove(elementRemoveBtn);

                BorderLan.Children.Add(button);
                gridColumns++;

                if (gridColumns == 2)
                {
                    gridColumns = 0;
                    gridRows++;
                }
            }
            if(languages.Count == 12)
                newLanguageBtn.IsEnabled = false;

            deleteLanguageBtn.IsEnabled = languages.Count > 0;
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
        private void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Closing window
        private void BtnContinue(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(actualLanguage, comboUnit.SelectedItem.ToString());
            this.Close();
            mainWindow.Show();
            mainWindow.Focus();
        } // Creating new window for learning words
        private void BtnNewUnit(object sender, RoutedEventArgs e)
        {
            newUnit = new NewUnit(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", actualLanguage), comboUnit)
            {
                Owner = this,
                ShowActivated = true
            };
            newUnit.Closed += (s, args) =>
            {
                IsEnabled = true;
                Focus();
                ReadComboBox(sender, e);
            };
            newUnit.Show();
            this.IsEnabled = false;
        } // Creating new window for adding new unit
        private void ReadComboBox(object sender, EventArgs e)
        {
            continueBtn.IsEnabled = true;
            newUnitBtn.IsEnabled = true;
            comboUnit.IsEnabled = true;
            comboUnit.Items.Clear();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", actualLanguage);
            List<string> units = new List<string>(Directory.GetDirectories(path).Select(Path.GetFileName).ToList());

            if (units.Count == 0)
            {
                continueBtn.IsEnabled = false;
                comboUnit.IsEnabled = false;
                deleteUnitBtn.IsEnabled = false;
            }
            else
                deleteUnitBtn.IsEnabled = true;

            foreach (var unit in units)
            {
                comboUnit.Items.Add(unit);
                comboUnit.SelectedIndex = 0;
            }
        } // Reading units from folder and adding them to combobox
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if ((newLanguage != null && newLanguage.IsVisible) || (newUnit != null && newUnit.IsVisible))
                e.Cancel = true;
        } // Preventing from closing main window when new language window is open
        private void BtnDeleteUnit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ask = MessageBox.Show("Are you sure you want to delete this unit?", "Delete unit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ask == MessageBoxResult.No)
                return;

            string unit = comboUnit.SelectedItem.ToString();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", actualLanguage, unit);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                ReadComboBox(sender, e);
            }
        } // Deleting unit from folder 
        private void BtnDeleteLanguage(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ask = MessageBox.Show("Are you sure you want to delete this language?", "Delete language", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ask == MessageBoxResult.No)
                return;

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", actualLanguage);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                AddingFlagsAsButtons();
            }
            comboUnit.Items.Clear();
            continueBtn.IsEnabled = false;
            newUnitBtn.IsEnabled = false;
            deleteUnitBtn.IsEnabled = false;
            deleteLanguageBtn.IsEnabled = false;
            UIElement elementRemoveBtn = FlagGrid.Children.Cast<UIElement>().FirstOrDefault(x => Grid.GetColumn(x) == 1 && Grid.GetRow(x) == 0);

            if (elementRemoveBtn != null)
            {
                FlagGrid.Children.Remove(elementRemoveBtn);
            }
            actualLanguage = "";

        } // Deleting language from folder
    }
}
