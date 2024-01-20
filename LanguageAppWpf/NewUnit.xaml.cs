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
    public partial class NewUnit : Window
    {
        private string directPath;
        ComboBox ComboBox;
        public NewUnit(string path, ComboBox box)
        {
            InitializeComponent();
            directPath = path;
            ComboBox = box;
        }
        private void BtnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnAddUnit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UnitName.Text != "" && !ComboBox.Items.Contains(UnitName.Text))
                {
                    string path = System.IO.Path.Combine(directPath, UnitName.Text);
                    System.IO.Directory.CreateDirectory(path);
                    this.Close();
                }
                else if (UnitName.Text == "")
                {
                    throw new FormatException("You have to enter the name of unit");
                }
                else if (ComboBox.Items.Contains(UnitName.Text))
                {
                    throw new FormatException("This unit already exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
