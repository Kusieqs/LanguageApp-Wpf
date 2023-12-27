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
    /// Logika interakcji dla klasy NewUnit.xaml
    /// </summary>
    public partial class NewUnit : Window
    {
        private string lan;
        private string directPath;
        public NewUnit(string language)
        {
            lan = language;
            directPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", language);
            InitializeComponent();
        }

        private void BtnExit(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddUnit(object sender, RoutedEventArgs e)
        {

        }
    }
}
