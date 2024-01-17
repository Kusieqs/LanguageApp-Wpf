using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window, IFile
    {
        private int review;
        private string lan;
        private string unit;
        private string directPath;
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
            this.directPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LanguageAppWpf", lan, unit,"Data.json");
            this.Loaded += LoadingData;
            this.Activated += ActivData;
        }


        private void BtnAddWord(object sender, RoutedEventArgs e)
        {
            addWords = new AddWords();
            addWords.Owner = this;
            addWords.Show();
            addWords.Focus();
            this.IsEnabled = false;
            addWords.Closed += (s, args) => this.IsEnabled = true;
        }
        private void BtnReview(object sender, RoutedEventArgs e)
        {
            reviewWindow = new Review();
            reviewWindow.Owner = this;
            reviewWindow.Show();
            reviewWindow.Focus();
            this.IsEnabled = false;
            reviewWindow.Closed += (s, args) => this.IsEnabled = true;
        }
        private void BtnListOfWords(object sender, RoutedEventArgs e)
        {
            listOfWords = new ListOfWords();
            listOfWords.Owner = this;
            listOfWords.Show();
            listOfWords.Focus();
            this.IsEnabled = false;
            listOfWords.Closed += (s, args) => this.IsEnabled = true;
        }
        private void BtnChangeLanguage(object sender, RoutedEventArgs e)
        {
            LanguageChoose languageChoose = new LanguageChoose();
            languageChoose.Show();
            languageChoose.Focus();
            this.Close();
        }

        private void BtnDownWriteToJson(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            Random random = new Random();
            string pattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            if(words.Count == 0)
            {
                MessageBox.Show("No words to down write","Error",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                do
                {
                    string word = "";
                    for (int i = 0; i < 10; i++)
                    {
                        word += pattern[random.Next(0, pattern.Length)];
                    }

                    if (System.IO.File.Exists(System.IO.Path.Combine(path, word)))
                        continue;
                    else
                    {
                        string json = JsonConvert.SerializeObject(words);
                        System.IO.File.WriteAllText(System.IO.Path.Combine(path, word), json);
                        MessageBox.Show("File was created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                } while (true);
            }
        }
        private void BtnReadJson(object sender, RoutedEventArgs e)
        {
            readJsonFile = new ReadJsonFile(words);
            readJsonFile.Owner = this;
            readJsonFile.Show();
            readJsonFile.Focus();
            this.IsEnabled = false;
            readJsonFile.Closed += (s, args) => this.IsEnabled = true;
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
            NumberOfReview.Text = review.ToString();
            
            MostCorrectAndUncorrect(count);
        }
        private void ActivData(object sender, EventArgs e)
        {
            MostCorrectAndUncorrect(words.Count);
            NumberOfWords.Text = words.Count.ToString();
            NumberOfCorrect.Text = words.Select(x => x.Correct).Count().ToString();
            NumberOfUncorrect.Text = words.Select(x => x.Mistake).Count().ToString();
            NumberOfReview.Text = review.ToString();
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

            if(string.IsNullOrEmpty(FirstOne.Text))
            {
                FirstOne.Text = "You have to add words";
            }
            if (string.IsNullOrEmpty(SecondOne.Text))
            {
                SecondOne.Text = "You have to add words";
            }
            if (string.IsNullOrEmpty(ThirdOne.Text))
            {
                ThirdOne.Text = "You have to add words";
            }

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if ((reviewWindow!= null && reviewWindow.IsVisible) || (readJsonFile != null && readJsonFile.IsVisible) || (addWords != null&&addWords.IsVisible) || (listOfWords!= null && listOfWords.IsVisible))
            {
                e.Cancel = true;
            }
        }
    }    
}
