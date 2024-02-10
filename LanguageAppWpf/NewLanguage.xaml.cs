using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace LanguageAppWpf
{
    /// <summary>
    /// Logika interakcji dla klasy NewLanguage.xaml
    /// </summary>
    public partial class NewLanguage : Window
    {
        private List<string> languages = new List<string>
        {
            NameOfLanguage.Polish.ToString(),
            NameOfLanguage.English.ToString(),
            NameOfLanguage.German.ToString(),
            NameOfLanguage.French.ToString(),
            NameOfLanguage.Spanish.ToString(),
            NameOfLanguage.Italian.ToString(),
            NameOfLanguage.Russian.ToString(),
            NameOfLanguage.Portuguese.ToString(),
            NameOfLanguage.Swedish.ToString(),
            NameOfLanguage.Norwegian.ToString(),
            NameOfLanguage.Chinese.ToString(),
            NameOfLanguage.Arabic.ToString(),
        };
        
        public NewLanguage()
        {
            InitializeComponent();
            this.Loaded += AddingList;
        }
        public void BtnNewLanguage(object sender, RoutedEventArgs e)
        {
            if(ComboLan.SelectedItem != null)
            {
                string selectedLanguage = ComboLan.SelectedItem.ToString();
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure to add {selectedLanguage}", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf");
                    Directory.CreateDirectory(System.IO.Path.Combine(path,selectedLanguage));
                    this.Close();
                }
            }
        } // Adding new language as new directory
        public void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Closing window
        private void AddingList(object sender, RoutedEventArgs e)
        {
            List<string> languagesJson = new List<string>(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"LanguageAppWpf")).Select(System.IO.Path.GetFileName).ToList());
            languagesJson = languages.Except(languagesJson).ToList();
            foreach (string language in languagesJson)
            {
                ComboLan.Items.Add(language);
            }
            if (ComboLan.Items.Count == 0)
            {
                AddLan.IsEnabled = false;
                ComboLan.IsEnabled = false;
            }
        } // Adding list of languages to combobox
    }
}
