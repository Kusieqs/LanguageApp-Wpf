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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string lan;
        private string unit;
        private string directPath;
        public MainWindow(string lan, string unit)
        {
            InitializeComponent();
            this.lan = lan;
            this.unit = unit;
            this.directPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", lan, unit,"Data.json");
            this.Loaded += LoadingData;
        }

        private void BtnAddWord(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReview(object sender, RoutedEventArgs e)
        {

        }

        private void BtnListOfWords(object sender, RoutedEventArgs e)
        {

        }

        private void BtnChangeLanguage(object sender, RoutedEventArgs e)
        {
            this.Close();
            LanguageChoose languageChoose = new LanguageChoose();
            languageChoose.Show();
        }

        private void BtnDownWriteToJson(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReadJson(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SwitchButton(object sender, RoutedEventArgs e)
        {
            if(WordsList.Text == "Most uncorrect words")
            {
                WordsList.Text = "Most correct words";
            }
            else
            {
                WordsList.Text = "Most uncorrect words";
            }

            /// wczytywanie danych z pliku json do statystyk
        }
        private void LoadingData(object sender, RoutedEventArgs e)
        {
            Language.Text = lan;
            Unit.Text = unit;
        }
    }
}
