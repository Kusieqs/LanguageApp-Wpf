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
        private List<Language> languages = new List<Language>
        {
            new Language(NameOfLanguage.English),
            new Language(NameOfLanguage.Polish),
            new Language(NameOfLanguage.German),
            new Language(NameOfLanguage.French),
            new Language(NameOfLanguage.Spanish),
            new Language(NameOfLanguage.Italian),
            new Language(NameOfLanguage.Russian),
            new Language(NameOfLanguage.Portuguese),
            new Language(NameOfLanguage.Swedish),
            new Language(NameOfLanguage.Norwegian),
            new Language(NameOfLanguage.Chinese),
            new Language(NameOfLanguage.Arabic),
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
                    string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", "Languages");
                    string jsonRead = File.ReadAllText(path);
                    List<Language> languagesJson = JsonConvert.DeserializeObject<List<Language>>(jsonRead);
                    languagesJson.Add(new Language((NameOfLanguage)Enum.Parse(typeof(NameOfLanguage), selectedLanguage)));
                    string jsonWrite = JsonConvert.SerializeObject(languagesJson);
                    File.WriteAllText(path, jsonWrite);
                    this.Close();
                }
            }
        } // Adding new language to json file
        public void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Closing window
        private void AddingList(object sender, RoutedEventArgs e)
        {
            string appDataFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", "Languages");
            string jsonRead = File.ReadAllText(appDataFolder);
            List<Language> languagesJson = JsonConvert.DeserializeObject<List<Language>>(jsonRead);
            languagesJson = EqualsLists(languages, languagesJson);
            foreach (Language language in languagesJson)
            {
                ComboLan.Items.Add(language.nameOfLanguage);
            }
            if (ComboLan.Items.Count == 0)
            {
                AddLanguage.IsEnabled = false;
            }
        } // Adding list of languages to combobox
        private List<Language> EqualsLists(List<Language> l1, List<Language> l2)
        {
            bool equal = false;
            List<Language> l3 = new List<Language>();
            foreach (Language lan1 in l1)
            {
                foreach (Language lan2 in l2)
                {
                    if(lan1.nameOfLanguage == lan2.nameOfLanguage)
                    {
                        equal = true;
                        break;
                    }
                }
                if (!equal)
                {
                    l3.Add(lan1);
                }
                equal = false;
            }
            return l3;
        } // Checking if lists are equal and returning list of languages that are not in json file
    }
}
