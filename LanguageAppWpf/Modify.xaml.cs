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
        public Modify(int index)
        {
            InitializeComponent();
            this.index = index;
            this.Loaded += LoadInformations;
        }
        private void AcceptBtn(object sender, RoutedEventArgs e)
        {
            MainWindow.words[index].WordName = Word.Text;
            MainWindow.words[index].Translation = Translation.Text;
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
            if(Translation.Text.Length > 0 && Regex.IsMatch(Translation.Text, @"^[a-zA-Z\s]+$"))
            {
                Translation.Text = (char.ToUpper(Translation.Text[0]) + Translation.Text.Substring(1)).TrimStart();
                Translation.SelectionStart = Translation.Text.Length;
                translationBox = true;
            }
            else if (Translation.Text.Length == 0)
                translationBox = false;
            else if (Translation.Text.Length > 0 && !Regex.IsMatch(Translation.Text, @"^[a-zA-Z\s]+$"))
                Translation.Text = Translation.Text.Remove(Translation.Text.Length - 1);
            ButtonActive();
        }
        private void WordTextChanged(object sender, TextChangedEventArgs e)
        {
            if(Word.Text.Length > 0 && Regex.IsMatch(Word.Text, @"^[a-zA-Z\s]+$"))
            {
                Word.Text = (char.ToUpper(Word.Text[0]) + Word.Text.Substring(1)).TrimStart();
                Word.SelectionStart = Word.Text.Length;
                wordBox = true;
            }
            else if (Word.Text.Length == 0)
                wordBox = false;
            else if (Word.Text.Length > 0 && !Regex.IsMatch(Word.Text, @"^[a-zA-Z\s]+$"))
                Word.Text = Word.Text.Remove(Word.Text.Length - 1);
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
