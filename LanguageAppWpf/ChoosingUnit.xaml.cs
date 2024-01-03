using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logika interakcji dla klasy ChoosingUnit.xaml
    /// </summary>
    public partial class ChoosingUnit : Window
    {
        private NewUnit newUnit;
        private LanguageChoose languageChoose;
        private string lan;
        private string directPath;
        public ChoosingUnit(string lan, LanguageChoose languageChoose)
        { 
            this.languageChoose = languageChoose;
            this.lan = lan;
            directPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", lan);
            this.Loaded += ActivData;
            this.Activated += ActivData;
            InitializeComponent();
        }
        private void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ContinueBtn(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(lan, ComboUnit.Text);
            mainWindow.Show();
            this.Close();
            languageChoose.Close();
        }
        private void NewUnitBtn(object sender, RoutedEventArgs e)
        {
            newUnit = new NewUnit(directPath, ComboUnit);
            newUnit.Owner = this;
            newUnit.Show();
            this.IsEnabled = false;
            newUnit.Closed += (s, args) => this.IsEnabled = true;
        }
        private void ActivData(object sender, EventArgs e) // Adding flags as buttons when window is loaded or activated 
        {
            ComboUnit.Items.Clear();
            List<string> units = new List<string>(Directory.GetDirectories(directPath).Select(System.IO.Path.GetFileName).ToList());
            foreach(string unit in units)
            {
                ComboUnit.Items.Add(unit);
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if(newUnit != null && newUnit.IsVisible)
            {
                e.Cancel = true;
            } 
        }

    }
}
