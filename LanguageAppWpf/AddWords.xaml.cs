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
    /// Logika interakcji dla klasy AddWords.xaml
    /// </summary>
    public partial class AddWords : Window
    {
        private NameOfLanguage _language;
        private string _unit;
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
        }

        private void AddWordBtn(object sender, RoutedEventArgs e)
        {
            if(WordTextBox.Text == "" || TranslationTextBox.Text == "")
            {
                MessageBox.Show("Word or translation is empty","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            Category category = (Category)Enum.Parse(typeof(Category), TypeComboBox.Text);
            Word word = new Word(_language,WordTextBox.Text,TranslationTextBox.Text,category,_unit);
            MainWindow.words.Add(word); 
            WordTextBox.Text = "";
            TranslationTextBox.Text = "";

        }
        private void LoadComboBox(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Enum.GetNames(typeof(Category)).Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = Enum.GetNames(typeof(Category))[i];
                TypeComboBox.Items.Add(item);
            }
            TypeComboBox.SelectedIndex = 0;
        }
    }
}
