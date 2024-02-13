using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Configuration;

namespace LanguageAppWpf
{
    public partial class AddWords : Window
    {
        private NameOfLanguage _language;
        private string _unit;
        private bool WordBox = false;
        private bool TranslationBox = false;
        public AddWords(string lan, string unit)
        {
            InitializeComponent();
            this.Loaded += LoadComboBox;
            _language = (NameOfLanguage)Enum.Parse(typeof(NameOfLanguage), lan);
            _unit = unit;
        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Exit button
        private void AddWordBtn(object sender, RoutedEventArgs e)
        {
            Category category = (Category)Enum.Parse(typeof(Category), TypeComboBox.Text);
            Word word = new Word(_language,WordTextBox.Text.Trim(),TranslationTextBox.Text.Trim(),category,_unit);
            MainWindow.words.Add(word); 
            MainWindow.SaveData();
            WordTextBox.Text = "";
            TranslationTextBox.Text = "";
            AddWord.IsEnabled = false;

        } // Add word button
        private void LoadComboBox(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Enum.GetNames(typeof(Category)).Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = Enum.GetNames(typeof(Category))[i];
                TypeComboBox.Items.Add(item);
            }
            TypeComboBox.SelectedIndex = 0;
        } // Load combobox
        private void TranslationTextChanged(object sender, TextChangedEventArgs e)
        {
            if(TranslationTextBox.Text.Length > 0 && Regex.IsMatch(WordTextBox.Text, @"^[a-zA-Z\s]$")
            {
                TranslationTextBox.Text = (char.ToUpper(TranslationTextBox.Text[0]) + TranslationTextBox.Text.Substring(1)).Trim();
                TranslationTextBox.SelectionStart = TranslationTextBox.Text.Length;
                TranslationBox = true;
            }
            else
                TranslationBox = false;
            CheckBtn();
        } // Capitalizing first letter and checking if translation box is not empty
        private void WordTextChanged(object sender, TextChangedEventArgs e)
        {

            if(WordTextBox.Text.Length > 0 && Regex.IsMatch(WordTextBox.Text, @"^[a-zA-Z\s]$"))
            {
                WordTextBox.Text = (char.ToUpper(WordTextBox.Text[0]) + WordTextBox.Text.Substring(1)).Trim();
                WordTextBox.SelectionStart = WordTextBox.Text.Length;
                WordBox = true;
            }
            else
                WordBox = false;
            CheckBtn();
        } // Capitalizing first letter and checking if word box is not empty
        private void CheckBtn()
        {
            if(WordBox && TranslationBox)
                AddWord.IsEnabled = true;
            else
                AddWord.IsEnabled = false;
        } // Checking if both boxes are not empty and enabling add word button
    }
}
