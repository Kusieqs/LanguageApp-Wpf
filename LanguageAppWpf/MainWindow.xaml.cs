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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int review;
        private string lan;
        private string unit;
        private string directPath;
        private List<Word> words;
        private bool switcher = false;
        public MainWindow(string lan, string unit)
        {
            InitializeComponent();
            this.lan = lan;
            this.unit = unit;
            this.directPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", lan, unit,"Data.json");
            this.Loaded += LoadingData;
            this.Activated += ActivData;
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
                switcher = true;
            }
            else
            {
                WordsList.Text = "Most uncorrect words";
                switcher = false;
            }

            MostCorrectAndUncorrect(words.Count);
        }
        private void LoadingData(object sender, RoutedEventArgs e)
        {
            Language.Text = lan;
            Unit.Text = unit;

            if (System.IO.File.Exists(directPath))
            {
                string jsonRead = System.IO.File.ReadAllText(directPath);
                words = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Word>>(jsonRead);
            }
            else
            {
                words = new List<Word>();
            }

            int count = words.Count;
            NumberOfWords.Text = count.ToString();
            NumberOfCorrect.Text = words.Select(x => x.Correct).Count().ToString();
            NumberOfUncorrect.Text = words.Select(x => x.Mistake).Count().ToString();

            if(System.IO.File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf","Review.txt")))
            {
                string readFile = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", "Review.txt"));
                review = int.Parse(readFile);
            }
            else
            {
                review = 0;
            }

            MostCorrectAndUncorrect(count);
        }
        private void ActivData(object sender, EventArgs e)
        {

        }
        private void MostCorrectAndUncorrect(int count)
        {
            if(switcher == false)
            {
                switch (count)
                {
                    case 0:
                        break;
                    case 1:
                        FirstOne.Text = words[0].WordName;
                        MistakeOne.Text = words[0].Mistake.ToString();
                        break;
                    case 2:
                        List<Word> sortedWords = words.OrderByDescending(x => x.Mistake).ToList();
                        FirstOne.Text = sortedWords[0].WordName;
                        SecondOne.Text = sortedWords[1].WordName;
                        MistakeOne.Text = sortedWords[0].Mistake.ToString();
                        MistakeTwo.Text = sortedWords[1].Mistake.ToString();
                        break;
                    default:
                        List<Word> sortedWords2 = words.OrderByDescending(x => x.Mistake).ToList();
                        FirstOne.Text = sortedWords2[0].WordName;
                        SecondOne.Text = sortedWords2[1].WordName;
                        ThirdOne.Text = sortedWords2[2].WordName;
                        MistakeOne.Text = sortedWords2[0].Mistake.ToString();
                        MistakeTwo.Text = sortedWords2[1].Mistake.ToString();
                        MistakeThree.Text = sortedWords2[2].Mistake.ToString();
                        break;
                }
            }
            else
            {
                switch (count)
                {
                    case 0:
                        break;
                    case 1:
                        FirstOne.Text = words[0].WordName;
                        MistakeOne.Text = words[0].Mistake.ToString();
                        break;
                    case 2:
                        List<Word> sortedWords = words.OrderBy(x => x.Mistake).ToList();
                        FirstOne.Text = sortedWords[0].WordName;
                        SecondOne.Text = sortedWords[1].WordName;
                        MistakeOne.Text = sortedWords[0].Mistake.ToString();
                        MistakeTwo.Text = sortedWords[1].Mistake.ToString();
                        break;
                    default:
                        List<Word> sortedWords2 = words.OrderBy(x => x.Mistake).ToList();
                        FirstOne.Text = sortedWords2[0].WordName;
                        SecondOne.Text = sortedWords2[1].WordName;
                        ThirdOne.Text = sortedWords2[2].WordName;
                        MistakeOne.Text = sortedWords2[0].Mistake.ToString();
                        MistakeTwo.Text = sortedWords2[1].Mistake.ToString();
                        MistakeThree.Text = sortedWords2[2].Mistake.ToString();
                        break;
                }
            }

            if(FirstOne.Text == null)
            {
                FirstOne.Text = "You have to add words";
            }
            if (SecondOne.Text == null)
            {
                SecondOne.Text = "You have to add words";
            }
            if (ThirdOne.Text == null)
            {
                ThirdOne.Text = "You have to add words";
            }

        }
    }            
}
