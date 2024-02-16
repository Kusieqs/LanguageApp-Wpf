using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;

namespace LanguageAppWpf
{
    public partial class Review : Window
    {
        private int loop = 0;
        private int wordCount = 0;
        private bool theme = false;
        private bool level = false;
        private bool mode = false;
        private string modeName = "";
        List<Word> wordList = new List<Word>();
        List<Word> listToReview = new List<Word>();
        Dictionary<string, int> wordDictionary = new Dictionary<string, int>
        {
            {"Easy", 15 },
            {"Medium", 30 },
            {"Hard", MainWindow.words.Count },
            {"Mistake", 20}
        }; // Dictionary with the number of words for each level
        private string path;
        public Review(string path)
        {
            InitializeComponent();
            TranslationBox.PreviewKeyDown += ReviewWord;
            this.path = path;
        }

        private void CheckedTheme(object sender, RoutedEventArgs e)
        {
            theme = true;
            StartBtnActive();
            string name = (sender as CheckBox).Name;
            wordList = wordList.Union(MainWindow.words.Where(x => x.Category.ToString() == name)).ToList();
        } // Checking and unchecking the theme of the review
        private void UncheckedTheme(object sender, RoutedEventArgs e)        
        {
            int row = 0, column = 1, count = 0;
            for(int i = 1; i < 9; i++)
            {
                if ((Theme.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == column) as CheckBox).IsChecked == false)
                    count++;
                if (i == 3)
                {
                    column = 3;
                    row = 0;
                    continue;
                }
                row++;
            }

            if (count == 8)
            {
                theme = false;
                StartBtnActive();
            }
            string name = (sender as CheckBox).Name;
            wordList = wordList.Where(x => x.Category.ToString() != name).ToList();
        } // Checking and unchecking the theme of the review
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
                for(int i = 1; i < 5; i++)
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
        } // Checking the level of the review
        private void CheckedMode(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (sender as CheckBox);
            checkBox.IsEnabled = false;
            int senderRow = Grid.GetRow((sender as CheckBox));
            if(!mode)
            {
                mode = true;
                StartBtnActive();
            }
            else
            {
                for(int i = 1; i < 4; i++)
                {
                    if(Mode.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == i && Grid.GetColumn(x) == 1) is CheckBox checkBox1 && i != senderRow)
                    {
                        (Mode.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == i && Grid.GetColumn(x) == 1) as CheckBox).IsEnabled = true;
                        (Mode.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == i && Grid.GetColumn(x) == 1) as CheckBox).IsChecked = false;
                    }
                }
            }
            modeName = (Mode.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == senderRow && Grid.GetColumn(x) == 0) as TextBlock).Text;
        }  // Checking the mode of the review
        private void StartBtnActive()
        {
            if(theme && level && mode)
            {
                Start.IsEnabled = true;
                return;
            }
            Start.IsEnabled = false;
        } // Checking if the start button should be active
        private void StartBtn(object sender, RoutedEventArgs e)
        {
            MainWindow.review++;
            File.WriteAllText(path, MainWindow.review.ToString());

            #region Freezing the buttons
            Restart.IsEnabled = true;
            Stop.IsEnabled = true;
            Start.IsEnabled = false;
            TranslationBox.IsEnabled = true;
            Theme.IsEnabled = false;
            Level.IsEnabled = false;
            Mode.IsEnabled = false;
            #endregion Freezing the buttons

            Random random = new Random();
            List<Word> copyWordList = wordList.ToList();

            foreach (var item in wordList)
            {
                int index = random.Next(0, copyWordList.Count);
                listToReview.Add(copyWordList[index]);
                copyWordList.RemoveAt(index);
            }
            listToReview = listToReview.Distinct().ToList();

            if (wordCount == 20)
                listToReview = listToReview.OrderBy(x => x.Mistake).ToList();
            else
            {
                if(listToReview.Count < wordCount)
                    listToReview = listToReview.GetRange(0, listToReview.Count);
                else
                    listToReview = listToReview.GetRange(0, wordCount);
            }
            ModeChoosed();

        } // Starting the review
        private bool CompareWords(string input, string target)
        {
            return input.ToLower() == target.ToLower();
        }// Comparing the words
        private void ReviewWord(object sender, KeyEventArgs e)
        {
            bool mistake = false;
            if (e.Key == Key.Enter)
            {
                Word word = listToReview[loop];
                switch (modeName)
                {
                    case "Word":
                        mistake = !CompareWords(TranslationBox.Text, word.Translation);
                        break;
                    case "Translation":
                        mistake = !CompareWords(TranslationBox.Text, word.WordName);
                        break;
                    case "Mix":
                        if(WordName.Text.ToLower() == word.WordName.ToLower())
                            mistake = !CompareWords(TranslationBox.Text, word.Translation);
                        else
                            mistake = !CompareWords(TranslationBox.Text, word.WordName);

                        break;
                }

                if(mistake)
                {
                    listToReview[loop].Mistake++;
                    LastResult.Text = "Uncorrect";
                    LastResult.Foreground = Brushes.Red;
                }
                else
                {
                    listToReview[loop].Correct++;
                    LastResult.Text = "Correct";
                    LastResult.Foreground = Brushes.Green;
                }

                WordList.Text = listToReview[loop].WordName;
                TranslationList.Text = listToReview[loop].Translation;
                loop++;
                TranslationBox.Text = "";
                if (loop == listToReview.Count)
                {
                    EnabledButtons();
                    return;
                }
                ModeChoosed();
            }
        } // Reviewing the words
        private void ModeChoosed() 
        {
            if(listToReview.Count == 0)
            {
                EnabledButtons();
                MessageBox.Show("No words in review", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            switch (modeName)
            {
                case "Word":
                    WordName.Text = listToReview[loop].WordName;
                    break;
                case "Translation":
                    WordName.Text = listToReview[loop].Translation;
                    break;
                case "Mix":
                    Random random = new Random();
                    int index = random.Next(0, 2);
                    if(index == 0)
                        WordName.Text = listToReview[loop].WordName;
                    else
                        WordName.Text = listToReview[loop].Translation;
                    break;
            }
        } // Choosing the mode of the review
        private void EnabledButtons()
        {
            loop = 0;
            WordName.Text = "";
            Theme.IsEnabled = true;
            Level.IsEnabled = true;
            Mode.IsEnabled = true;
            TranslationBox.IsEnabled = false;
            Start.IsEnabled = true;
            Restart.IsEnabled = false;
            Stop.IsEnabled = false;
        } // Changing the buttons to the default state
        private void RestartBtn(object sender, RoutedEventArgs e)
        {
            TranslationBox.Text = "";
            loop = 0;
            ModeChoosed();
        } // Restarting the review
        private void StopBtn(object sender, RoutedEventArgs e)
        {
            EnabledButtons();
        } // Stopping the review, Changing the buttons to the default state
    }
}
