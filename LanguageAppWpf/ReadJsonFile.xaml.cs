using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace LanguageAppWpf
{
    /// <summary>
    /// Logika interakcji dla klasy ReadJsonFile.xaml
    /// </summary>
    public partial class ReadJsonFile : Window
    {
        public List<Word> words { get; set; }
        public ReadJsonFile(List<Word> words)
        {
            InitializeComponent();
            this.words = words;
        }
        private void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRead(object sender, RoutedEventArgs e)
        {
            if(txtFileName.Text == "")
            {
                MessageBox.Show("Text box is empty","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathFile = System.IO.Path.Combine(path, txtFileName.Text + ".json");
            if (!System.IO.File.Exists(pathFile))
            {
                MessageBox.Show("File doesn't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int repeat = 0;
                string json = System.IO.File.ReadAllText(pathFile);
                List<Word> wordsFromFile = JsonConvert.DeserializeObject<List<Word>>(json);
                if(wordsFromFile.Count == 0)
                {
                    MessageBox.Show("File is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (var word in wordsFromFile)
                {
                    if (!words.Any(x => x.WordName.ToLower() == word.WordName.ToLower() && x.Translation.ToLower() == word.Translation.ToLower()))
                    {
                        words.Add(word);
                    }
                    else
                        repeat++;
                }
                MessageBox.Show($"File has been read\nCount of words repeat: {repeat}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.words = words;

            }
            catch(Exception ex)
            {
                MessageBox.Show("File can't be read.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // sprawdzanie czy jest pusty text, czy jest taki plik na pulpicie, sprawdzenie czy slowka sie powtarzaja, przypisanie tych slowek do listy, zapisanie do pliku (unitu)
        }

    }
}
