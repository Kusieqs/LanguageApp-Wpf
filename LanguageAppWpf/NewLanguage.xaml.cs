﻿using System;
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
    /// Logika interakcji dla klasy NewLanguage.xaml
    /// </summary>
    public partial class NewLanguage : Window
    {
        public NewLanguage()
        {
            InitializeComponent();
            this.Loaded += AddingList;
        }
        public void BtnNewLanguage(object sender, RoutedEventArgs e)
        {

        }
        private void AddingList(object sender, RoutedEventArgs e)
        {

        }
    }
}
