using Newtonsoft.Json.Bson;
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
    public partial class Modify : Window
    {
        private int index;
        public Modify(int index)
        {
            InitializeComponent();
            this.index = index;
            this.Loaded += LoadInformations;
        }

        private void AcceptBtn(object sender, RoutedEventArgs e)
        {
            if(Word.Text == "" || Translation.Text == "")
            {
                MessageBox.Show("Word or translation is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
    }
}
