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
        private string translationData;
        private string wordData;
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
            string wordName = WordTextBox.Text.Trim().ToLower();
            wordName = char.ToUpper(wordName[0]) + wordName.Substring(1);
            string translation = TranslationTextBox.Text.Trim().ToLower();
            translation = char.ToUpper(translation[0]) + translation.Substring(1);

            Word word = new Word(_language,wordName,translation,category,_unit);
            if (!MainWindow.words.Contains(word))
            {
                MainWindow.words.Add(word);
                MainWindow.SaveData();
            }
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
            if(TranslationTextBox.Text.Length > 0 && Regex.IsMatch(TranslationTextBox.Text, @"^[a-zA-Z,'\s-ąćęłńóśźżĄĆĘŁŃÓŚŹŻáéíóúÁÉÍÓÚñÑàèìòùÀÈÌÒÙèéîóòçàãçêíõôÀÃÇÊÍÕÔåäöÅÄÖæøåÆØÅ]+$"))
                TranslationBox = true;
            else if (TranslationTextBox.Text.Length == 0)
                TranslationBox = false;
            else if (TranslationTextBox.Text.Length > 0 && !Regex.IsMatch(TranslationTextBox.Text, @"^[a-zA-Z,'\s-ąćęłńóśźżĄĆĘŁŃÓŚŹŻáéíóúÁÉÍÓÚñÑàèìòùÀÈÌÒÙèéîóòçàãçêíõôÀÃÇÊÍÕÔåäöÅÄÖæøåÆØÅ]+$"))
            {
                TranslationTextBox.Text = translationData;
                TranslationTextBox.SelectionStart = TranslationTextBox.Text.Length;
            }

            translationData = TranslationTextBox.Text;

            CheckBtn();
        } // Capitalizing first letter and checking if translation box is not empty
        private void WordTextChanged(object sender, TextChangedEventArgs e)
        {

            if(WordTextBox.Text.Length > 0 && Regex.IsMatch(WordTextBox.Text, @"^[a-zA-Z,'\s-ąćęłńóśźżĄĆĘŁŃÓŚŹŻáéíóúÁÉÍÓÚñÑàèìòùÀÈÌÒÙèéîóòçàãçêíõôÀÃÇÊÍÕÔåäöÅÄÖæøåÆØÅ]+$"))
                WordBox = true;
            else if(WordTextBox.Text.Length == 0)
                WordBox = false;
            else if(WordTextBox.Text.Length > 0 && !Regex.IsMatch(WordTextBox.Text, @"^[a-zA-Z,'\s-ąćęłńóśźżĄĆĘŁŃÓŚŹŻáéíóúÁÉÍÓÚñÑàèìòùÀÈÌÒÙèéîóòçàãçêíõôÀÃÇÊÍÕÔåäöÅÄÖæøåÆØÅ]+$"))
            {
                WordTextBox.Text = wordData;
                WordTextBox.SelectionStart = WordTextBox.Text.Length;
            }

            wordData = WordTextBox.Text;

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
