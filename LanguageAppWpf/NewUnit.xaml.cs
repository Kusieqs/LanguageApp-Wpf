using System;
using System.Windows;
using System.Windows.Controls;


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
        } // Closing window
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
                    throw new FormatException("You have to enter the name of unit");
                else if (ComboBox.Items.Contains(UnitName.Text))
                    throw new FormatException("This unit already exists");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } // Adding new unit as new directory
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox textbox && textbox.Text.Length>0)
            {
                textbox.Text = char.ToUpper(textbox.Text[0]) + textbox.Text.Substring(1);
                textbox.SelectionStart = textbox.Text.Length;
            }
        } // Capitalizing first letter
    }
}
