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


namespace LanguageAppWpf
{
    public partial class LanguageChoose : Window
    {
        public LanguageChoose()
        {
            InitializeComponent();
            this.Loaded += AddingFlagsAsButtons;
        }
        private void BtnAddLanguage(object sender, RoutedEventArgs e)
        {

        }
        private void AddingFlagsAsButtons(object sender, RoutedEventArgs e)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = System.IO.Path.Combine(appDataFolder, "LanguageAppWpf");
            List<Language> languages = new List<Language>();
            ExistingFolder(path, languages);
        }
        private void ExistingFolder(string path, List<Language> languages)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Language english = new Language(NameOfLanguage.English, "eng");
                languages.Add(english);
                string jsonCreator = JsonConvert.SerializeObject(languages);
                File.WriteAllText(System.IO.Path.Combine(path, "Languages"), jsonCreator);
            }
            else
            {
                string json = File.ReadAllText(System.IO.Path.Combine(path, "Languages"));
                languages = JsonConvert.DeserializeObject<List<Language>>(json);
            }
        }
    }
}
