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
        List<Word> words = new List<Word>();
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

        }
    }
}
