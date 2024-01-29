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
    /// Logika interakcji dla klasy ListOfWords.xaml
    /// </summary>
    public partial class ListOfWords : Window
    {
        List<Word> list = new List<Word>();
        List<Word> actualList = MainWindow.words;
        public ListOfWords()
        {
            InitializeComponent();
            this.Loaded += LoadScrollView;
        }
        private List<Word> ListOfWordsToMethods(object sender)
        {
            string x = "";
            int row = 0;
            List<Word> words = MainWindow.words;
            List<Word> wordsToNewList = new List<Word>();
            if (sender is CheckBox checkBox)
            {
                row = Grid.GetRow(checkBox);
                if (SortBy.Children.Cast<UIElement>().FirstOrDefault(child => Grid.GetRow(child) == row && Grid.GetColumn(child) == 0) is TextBlock textBlock)
                {
                    x = textBlock.Text;
                    if (x.ToLower() == "correct")
                    {
                        if(Uncorrect.IsChecked == true)
                        {
                            Uncorrect.IsChecked = false;
                        }
                        return list.OrderBy(y => y.Correct).ToList();
                    }
                    else if(x.ToLower() == "Uncorrect")
                    {
                        if(Correct.IsChecked == true)
                        {
                            Correct.IsChecked = false;
                        }
                        return list.OrderBy(y => y.Mistake).ToList();
                    }
                    else if (x.ToLower() == "alfabetical" && Alfabetical.IsChecked == true)
                    {
                        return list.OrderBy(y => y.WordName).ToList();
                    }
                    else if(x.ToLower() == "alfabetical")
                    {
                        return list;
                    }
                }
            }

            foreach (Word word in words)
            {
                if (word.Category.ToString().ToLower() == x.ToLower())
                {
                    wordsToNewList.Add(word);
                }
            };  
             
            if(Alfabetical.IsChecked == true)
            {
                wordsToNewList.OrderBy(y => y.WordName).ToList();
            }
            else if(Correct.IsChecked == true)
            {
                wordsToNewList.OrderBy(y => y.Correct).ToList();
            }
            else if(Uncorrect.IsChecked == true)
            {
                wordsToNewList.OrderBy(y => y.Mistake).ToList();
            }
            return wordsToNewList;

        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            List<Word> wordsToAdd = ListOfWordsToMethods(sender);
            list = list.Union(wordsToAdd).ToList();
            ItemsScrollView(list);
        }
        private void Unchecked(object sender, RoutedEventArgs e)
        {
            List<Word> wordsToDelete = ListOfWordsToMethods(sender);
            list = list.Except(wordsToDelete).ToList();
            ItemsScrollView(list);
        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoadScrollView(object sender, RoutedEventArgs e)
        {
            ItemsScrollView(actualList);
        }
        private void Modify(object sender, RoutedEventArgs e)
        {

        }
        private void ItemsScrollView(List<Word> list)
        {
            if (ScrollList != null)
            {
                ScrollList.ClearValue(ScrollViewer.ContentProperty);
            }

            StackPanel panel = new StackPanel();
            foreach (Word word in list)
            {
                if (word != null)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = " " + word.WordName;
                    textBlock.Width = 280;
                    textBlock.Height = 30;
                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.FontSize = 20;
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.Foreground = Brushes.White;
                    stackPanel.Children.Add(textBlock);
                    TextBlock textBlock1 = new TextBlock();
                    textBlock1.Text = word.Translation;
                    textBlock1.Width = 280;
                    textBlock1.Height = 30;
                    textBlock1.TextAlignment = TextAlignment.Left;
                    textBlock1.VerticalAlignment = VerticalAlignment.Center;
                    textBlock1.FontSize = 20;
                    textBlock1.FontWeight = FontWeights.Bold;
                    textBlock1.Foreground = Brushes.White;
                    stackPanel.Children.Add(textBlock1);
                    TextBlock textBlock2 = new TextBlock();
                    textBlock2.Text = word.Mistake.ToString();
                    textBlock2.Width = 70;
                    textBlock2.Height = 30;
                    textBlock2.TextAlignment = TextAlignment.Left;
                    textBlock2.VerticalAlignment = VerticalAlignment.Center;
                    textBlock2.FontSize = 20;
                    textBlock2.FontWeight = FontWeights.Bold;
                    textBlock2.Foreground = Brushes.White;
                    stackPanel.Children.Add(textBlock2);
                    TextBlock textBlock3 = new TextBlock();
                    textBlock3.Text = word.Correct.ToString();
                    textBlock3.Width = 70;
                    textBlock3.Height = 30;
                    textBlock3.TextAlignment = TextAlignment.Left;
                    textBlock3.VerticalAlignment = VerticalAlignment.Center;
                    textBlock3.FontSize = 20;
                    textBlock3.FontWeight = FontWeights.Bold;
                    textBlock3.Foreground = Brushes.White;
                    stackPanel.Children.Add(textBlock3);
                    Button button = new Button();
                    Image image = new Image();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/iconList.png");
                    bitmap.EndInit();

                    image.Source = bitmap;
                    image.Stretch = Stretch.Fill;
                    button.Content = image;
                    button.Width = 30;
                    button.Height = 30;
                    button.Margin = new Thickness(20, 10, 20, 10);
                    button.Name = "iconList";
                    button.Background = Brushes.Transparent;
                    button.BorderBrush = Brushes.Transparent;
                    button.Foreground = Brushes.Transparent;
                    button.IsEnabled = true;

                    button.Click += Modify;
                    stackPanel.Children.Add(button);
                    panel.Children.Add(stackPanel);

                }
                ScrollList.Content = panel;

            }


        }

    }
}
