using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LanguageAppWpf
{
    public partial class MainWindow : Window
    {
        public static int review { get; set;}
        public string lan { get; set; }
        public string unit { get; set; }
        public static string directPath { get; set; }
        public string pathReview { get; set; }
        public static List<Word> words = new List<Word>();
        private bool switcher = false;
        private AddWords addWords;
        private Review reviewWindow;
        private ListOfWords listOfWords;
        private ReadJsonFile readJsonFile;
        public MainWindow(string lan, string unit)
        {
            InitializeComponent();
            this.lan = lan;
            this.unit = unit;
            directPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", lan, unit,"Data.json");
            pathReview = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", lan, unit, "Review");
            this.Loaded += LoadingData;
            this.Activated += ActivData;
        }
        private void BtnAddWord(object sender, RoutedEventArgs e)
        {
            addWords = new AddWords(lan, unit)
            {
                Owner = this
            };
            addWords.Show();
            addWords.Focus();
            this.IsEnabled = false;
            addWords.Closed += (s, args) =>
            {
                IsEnabled = true;
                Focus();
            };
        } // Add word button
        private void BtnReview(object sender, RoutedEventArgs e)
        {
            reviewWindow = new Review(pathReview)
            {
                Owner = this
            };
            reviewWindow.Show();
            reviewWindow.Focus();
            this.IsEnabled = false;
            reviewWindow.Closed += (s, args) =>
            {
                IsEnabled = true;
                Focus();
            };
        } // Review button 
        private void BtnListOfWords(object sender, RoutedEventArgs e)
        {
            listOfWords = new ListOfWords()
            {
                Owner = this
            };
            listOfWords.Show();
            listOfWords.Focus();
            this.IsEnabled = false;
            listOfWords.Closed += (s, args) =>
            {
                IsEnabled = true;
                Focus();
            };
        } // List of words button 
        private void BtnChangeLanguage(object sender, RoutedEventArgs e)
        {
            LanguageChoose languageChoose = new LanguageChoose();
            languageChoose.Show();
            languageChoose.Focus();
            this.Close();
        } // Change language button 
        private void BtnDownWriteToJson(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string pattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            if(words.Count == 0)
                MessageBox.Show("No words to down write","Error",MessageBoxButton.OK,MessageBoxImage.Information);
            else
            {
                Random random = new Random();
                do
                {
                    string pat = "";
                    for (int i = 0; i < 3; i++)
                    {
                        pat += pattern[random.Next(0, pattern.Length)];
                    }

                    string pathName = $"{lan} {unit} {pat}";
                    if (File.Exists(Path.Combine(path, pathName)))
                        continue;
                    else
                    {
                        string json = JsonConvert.SerializeObject(words);
                        File.WriteAllText(Path.Combine(path, pathName), json);
                        MessageBox.Show("File was created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                } while (true);
            }
        } // Down write to json file button 
        private void BtnReadJson(object sender, RoutedEventArgs e)
        {
            readJsonFile = new ReadJsonFile()
            {
                Owner = this
            };
            readJsonFile.Show();
            readJsonFile.Focus();
            this.IsEnabled = false;
            readJsonFile.Closed += (s, args) =>
            {
                IsEnabled = true;
                Focus();
            };
        } // Read json file button 
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Exit button
        private void SwitchButton(object sender, RoutedEventArgs e)
        {
            if(WordsList.Text == "Best words")
            {
                WordsList.Text = "Worst words";
                switcher = true;
            }
            else
            {
                WordsList.Text = "Best words";
                switcher = false;
            }

            MostCorrectAndUncorrect(words.Count);
        } // Switch button to show most correct or uncorrect words
        private void LoadingData(object sender, RoutedEventArgs e)
        {
            Language.Text = lan;
            Unit.Text = unit;

            if (File.Exists(directPath) && !string.IsNullOrEmpty(File.ReadAllText(directPath)))
            {
                string jsonRead = File.ReadAllText(directPath);
                words = JsonConvert.DeserializeObject<List<Word>>(jsonRead);
            }
            else
                words = new List<Word>();

            int count = words.Count;
            NumberOfWords.Text = count.ToString();
            NumberOfCorrect.Text = words.Sum(x => x.Correct).ToString();
            NumberOfUncorrect.Text = words.Sum(x => x.Mistake).ToString();

            ReviewNumber();
            MostCorrectAndUncorrect(count);
        } // Loading data 
        private void ReviewNumber()
        {
            if (File.Exists(pathReview))
            {
                string readFile = File.ReadAllText(pathReview);
                try
                {
                    int number = int.Parse(readFile);
                    review = number;
                }
                catch (Exception)
                {
                    review = 0;
                }
            }
            else
                review = 0;

            NumberOfReview.Text = review.ToString();
        }
        private void ActivData(object sender, EventArgs e)
        {
            MostCorrectAndUncorrect(words.Count);
            NumberOfWords.Text = words.Count.ToString();
            NumberOfCorrect.Text = words.Sum(x => x.Correct).ToString();
            NumberOfUncorrect.Text = words.Sum(x => x.Mistake).ToString();
            NumberOfReview.Text = review.ToString();
        } // Activ data 
        private void MostCorrectAndUncorrect(int count)
        {
            List<Word> sort = words.OrderByDescending(x => switcher ? x.Correct : x.Mistake).ToList();
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    SetTextAndMistakes(FirstOne,MistakeOne, sort,0);
                    break;
                case 2:
                    SetTextAndMistakes(FirstOne,MistakeOne,sort,0);
                    SetTextAndMistakes(SecondOne,MistakeTwo, sort,1);
                    break;
                default:
                    SetTextAndMistakes(FirstOne,MistakeOne, sort,0);
                    SetTextAndMistakes(SecondOne,MistakeTwo, sort,1);
                    SetTextAndMistakes(ThirdOne,MistakeThree, sort,2);
                    break;
            }
            SetDefaultTextIfEmpty();

        } // Most correct and uncorrect words 
        private void SetTextAndMistakes(TextBlock textBlock1, TextBlock textBlock2, List<Word> sortedWords, int index)
        {
            textBlock1.Text = sortedWords[index].WordName;
            textBlock2.Text = switcher ? sortedWords[index].Correct.ToString() : sortedWords[index].Mistake.ToString();
        } // Set text and mistakes
        private void SetDefaultTextIfEmpty()
        {
            string lack = "Lack of word";
            if(!words.Any(x => x.WordName == FirstOne.Text))
            {
                FirstOne.Text = lack;
                MistakeOne.Text = "";
            }
            if (!words.Any(x => x.WordName == SecondOne.Text))
            {
                SecondOne.Text = lack;
                MistakeTwo.Text = "";
            }
            if (!words.Any(x => x.WordName == ThirdOne.Text))
            {
                ThirdOne.Text = lack;
                MistakeThree.Text = "";
            }
        }// Set default text if there is no word in list 
        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(words);
            File.WriteAllText(directPath,json);
        } // Save data to json file     
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if ((reviewWindow!= null && reviewWindow.IsVisible) || (readJsonFile != null && readJsonFile.IsVisible) || (addWords != null&&addWords.IsVisible) || (listOfWords!= null && listOfWords.IsVisible))
                e.Cancel = true;
        } // On closing window event 
    }    
}
