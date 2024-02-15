using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.IO;

namespace LanguageAppWpf
{
    public partial class ReadJsonFile : Window
    {
        public List<Word> words  = MainWindow.words.ToList();
        public ReadJsonFile()
        {
            InitializeComponent();
        }

        private void BtnRead(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathFile = System.IO.Path.Combine(path, txtFileName.Text); 

            try
            {

                if (!File.Exists(pathFile))
                    throw new FormatException($"File not found {pathFile}");

                int repeat = 0;
                string json = File.ReadAllText(pathFile);
                List<Word> wordsFromFile = JsonConvert.DeserializeObject<List<Word>>(json);

                if(wordsFromFile.Count == 0)
                    throw new FormatException("File is empty");
           
                foreach (var word in wordsFromFile)
                {
                    if (!words.Any(x => x.WordName.ToLower() == word.WordName.ToLower() && x.Translation.ToLower() == word.Translation.ToLower() && x.Category == word.Category))
                        words.Add(word);
                    else
                        repeat++;
                }
                MessageBox.Show($"File has been read\nCount of words repeat: {repeat}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.words = words.ToList();
                MainWindow.SaveData();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } // Read button and read file from desktop and add to list
        private void ButtonAvtiveRead(object sender, TextChangedEventArgs e)
        {
            txtFileName.Text = txtFileName.Text.TrimStart();
            if (txtFileName.Text.Length > 0)
                ReadBtn.IsEnabled = true;
            else
                ReadBtn.IsEnabled = false;
        } // Button active and text changed
        private void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Exit button

    }
}
