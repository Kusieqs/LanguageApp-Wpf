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
    public partial class Review : Window
    {
        private int wordCount = 0;
        private bool mistake = false;
        private bool theme = false;
        private bool level = false;
        private bool mode = false;
        List<Word> wordList = new List<Word>();
        Dictionary<string, int> wordDictionary = new Dictionary<string, int>
        {
            {"Easy", 15 },
            {"Medium", 30 },
            {"Hard", MainWindow.words.Count },
            {"Mistake", 10}
        };
        public Review()
        {
            InitializeComponent();
        }

        private void CheckedTheme(object sender, RoutedEventArgs e)
        {
            theme = true;
            StartBtnActive();
            string name = (sender as CheckBox).Name;
            wordList = wordList.Union(MainWindow.words.Where(x => x.Category.ToString() == name)).ToList();
        }

        private void UncheckedTheme(object sender, RoutedEventArgs e)
        {
            int row = 0;
            int column = 1;
            for(int i = 0; i < 8; i++)
            {
                if (Theme.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == column) is CheckBox checkBox)
                {
                    theme = false;
                }
                if (i == 3)
                {
                    column = 3;
                    row = 0;
                    continue;
                }
                row++;
            }
            StartBtnActive();
            string name = (sender as CheckBox).Name;
            wordList = wordList.Where(x => x.Category.ToString() != name).ToList();
        }
        private void CheckedLvl(object sender, RoutedEventArgs e)
        {
            int senderRow = Grid.GetRow((sender as CheckBox));
            int senderColumn = Grid.GetColumn((sender as CheckBox));
            if(!level)
            {
                level = true;
                StartBtnActive();
            }
            else
            {
                int column = 1;
                for(int i = 0; i < 4; i++)
                {
                    if (Level.Children.Cast<UIElement>().First(x => Grid.GetColumn(x) == column && Grid.GetRow(x) == i) is CheckBox && senderRow != i)
                    {
                        (Level.Children.Cast<UIElement>().First(x => Grid.GetColumn(x) == column && Grid.GetRow(x) == i) as CheckBox).IsChecked = false;
                        (Level.Children.Cast<UIElement>().First(x => Grid.GetColumn(x) == column && Grid.GetRow(x) == i) as CheckBox).IsEnabled = true;
                    }
                }

            }

            CheckBox checkBox1 = (sender as CheckBox);
            checkBox1.IsEnabled = false;
            string name = (sender as CheckBox).Name;
            wordCount = wordDictionary[name];   

            if(name == "Mistake")
            {
                mistake = true;
            }
            else
            {
                mistake = false;
            }
        }

        private void UncheckedLvl(object sender, RoutedEventArgs e)
        {
        }

        private void StartBtnActive()
        {
            if(theme && level && mode)
            {
                Start.IsEnabled = true;
            }
        }
        private void StartBtn(object sender, RoutedEventArgs e)
        {

        }
        private void RestartBtn(object sender, RoutedEventArgs e)
        {

        }

        private void StopBtn(object sender, RoutedEventArgs e)
        {

        }

        private void Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
