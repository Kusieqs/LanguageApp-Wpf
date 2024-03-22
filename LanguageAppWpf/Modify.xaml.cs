using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace LanguageAppWpf
{
    public partial class Modify : Window
    {
        private int index;
        private bool translationBox = false;
        private bool wordBox = false;
        private string translationData;
        private string wordData;
        public Modify(int index)
        {
            InitializeComponent();
            this.index = index;
            this.Loaded += LoadInformations;
        }
        private void AcceptBtn(object sender, RoutedEventArgs e)
        {
            string wordName = Word.Text.Trim().ToLower();
            wordName = char.ToUpper(wordName[0]) + wordName.Substring(1);
            string translation = Translation.Text.Trim().ToLower();
            translation = char.ToUpper(translation[0]) + translation.Substring(1);

            MainWindow.words[index].WordName = wordName;
            MainWindow.words[index].Translation = translation;
            MainWindow.words[index].Category = (Category)Enum.Parse(typeof(Category), ComboBoxCat.Text);
            Close();
        }
        private void LoadInformations(object sender, RoutedEventArgs e)
        {
            Word.Text = MainWindow.words[index].WordName;
            Translation.Text = MainWindow.words[index].Translation;
            for (int i = 0; i < Enum.GetNames(typeof(Category)).Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = Enum.GetNames(typeof(Category))[i];
                ComboBoxCat.Items.Add(item);
            }
            ComboBoxCat.SelectedIndex = (int)MainWindow.words[index].Category;
        }

        private void TranslationTextChanged(object sender, TextChangedEventArgs e)
        {
            Changed(Translation, true);
        }
        private void WordTextChanged(object sender, TextChangedEventArgs e)
        {
            Changed(Word, false);
        }
        private void Changed(TextBox textBox, bool whichProperty)
        {
            if(textBox.Text.Length > 0 && Regex.IsMatch(textBox.Text, @"^[a-zA-Z,'\s-ąćęłńóśźżĄĆĘŁŃÓŚŹŻáéíóúÁÉÍÓÚñÑàèìòùÀÈÌÒÙèéîóòçàãçêíõôÀÃÇÊÍÕÔåäöÅÄÖæøåÆØÅ]+$"))
            {
                if(whichProperty)
                    translationBox = true;
                else
                    wordBox = true;
            }
            else if (textBox.Text.Length == 0)
            {
                if(whichProperty)
                    translationBox = false;
                else
                    wordBox = false;
            }
            else if(textBox.Text.Length > 0 && !Regex.IsMatch(textBox.Text, @"^[a-zA-Z,'\s-ąćęłńóśźżĄĆĘŁŃÓŚŹŻáéíóúÁÉÍÓÚñÑàèìòùÀÈÌÒÙèéîóòçàãçêíõôÀÃÇÊÍÕÔåäöÅÄÖæøåÆØÅ]+$"))
            {
                if(whichProperty)
                {
                    Translation.Text = translationData;
                    Translation.SelectionStart = Translation.Text.Length;
                }
                else
                {
                    Word.Text = wordData;
                    Word.SelectionStart = Word.Text.Length;
                }
            }

            if(whichProperty)
                translationData = Translation.Text;
            else
                wordData = Word.Text;

            ButtonActive();
        }
        private void ButtonActive()
        {
            if(translationBox && wordBox)
                Accept.IsEnabled = true;
            else
                Accept.IsEnabled = false;
        }
    }
}
