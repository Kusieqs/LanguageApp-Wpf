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
        private Modify modify;
        private Button buttonSender;
        private Dictionary<Button, Word> buttonWordDictionary = new Dictionary<Button, Word>();
        private ContextMenu myContextMenu;
        List<Word> actualList = MainWindow.words;
        public ListOfWords()
        {
            InitializeComponent();
            this.Loaded += LoadScrollView;
        }
        private (List<Word>,bool) ListOfWordsToMethods(object sender)
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
                            Correct.IsChecked = false;
                        }
                        return (actualList.OrderBy(y => y.Correct).ToList(),false);
                    }
                    else if(x.ToLower() == "uncorrect")
                    {
                        if(Correct.IsChecked == true)
                        {
                            Uncorrect.IsChecked = false;
                        }
                        return (actualList.OrderBy(y => y.Mistake).ToList(),false);
                    }
                    else if (x.ToLower() == "alfabetical" && Alfabetical.IsChecked == true)
                    {
                        return (actualList.OrderBy(y => y.WordName).ToList(),false);
                    }
                    else if (x.ToLower() == "alfabetical" && Alfabetical.IsChecked == false)
                    {
                        return (actualList,false);
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
            return (wordsToNewList, true);

        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            var items = ListOfWordsToMethods(sender);
            if(items.Item2 == true)
            {
                actualList = actualList.Union(items.Item1).ToList();
                ExceptionsWithSort();
            }
            else
            {
                actualList = items.Item1;
            }
            ItemsScrollView(actualList);
        }
        private void Unchecked(object sender, RoutedEventArgs e)
        {
            var items = ListOfWordsToMethods(sender);
            if (items.Item2 == true)
            {
                actualList = actualList.Except(items.Item1).ToList();
                ExceptionsWithSort();
            }
            else
            {
                actualList = items.Item1;
            }
            ItemsScrollView(actualList);
        }
        private void ExceptionsWithSort()
        {
            if (Alfabetical.IsChecked == true)
            {
                actualList = actualList.OrderBy(y => y.WordName).ToList();
            }
            else if (Correct.IsChecked == true)
            {
                actualList = actualList.OrderBy(y => y.Correct).ToList();
            }
            else if (Uncorrect.IsChecked == true)
            {
                actualList = actualList.OrderBy(y => y.Mistake).ToList();
            }
        }
        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoadScrollView(object sender, RoutedEventArgs e)
        {
            #region LoadCheckedBoxes
            Verb.IsChecked = true;
            Noun.IsChecked = true;
            Adjective.IsChecked = true;
            Conjunction.IsChecked = true;
            Adverb.IsChecked = true;
            Other.IsChecked = true;
            Pronun.IsChecked = true;
            Presposition.IsChecked = true;
            #endregion LoadCheckedBoxes

            ItemsScrollView(actualList);
        }
        private void Modify(object sender, RoutedEventArgs e)
        {
            buttonSender = sender as Button;
            myContextMenu.IsOpen = true;
        }
        private void ItemsScrollView(List<Word> list)
        {
            int count = 0;
            buttonWordDictionary.Clear();
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
                    button.Name = "iconList" + count;
                    count++;
                    button.Background = Brushes.Transparent;
                    button.BorderBrush = Brushes.Transparent;
                    button.Foreground = Brushes.Transparent;
                    button.IsEnabled = true;

                    buttonWordDictionary.Add(button, word);
                    button.Click += Modify;
                    myContextMenu = CreateContextMenu();
                    button.ContextMenu = myContextMenu;

                    stackPanel.Children.Add(button);
                    panel.Children.Add(stackPanel);

                }
                ScrollList.Content = panel;
            }
        }
        private ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
           

            MenuItem menuItem1 = new MenuItem();
            menuItem1.Header = "Edit word";
            menuItem1.Click += MenuItem_Click;

            MenuItem menuItem2 = new MenuItem();
            menuItem2.Header = "Delete word";
            menuItem2.Click += MenuItem_Click;

            MenuItem menuItem3 = new MenuItem();
            menuItem3.Header = "Delete counting";
            menuItem3.Click += MenuItem_Click;

            contextMenu.Items.Add(menuItem1);
            contextMenu.Items.Add(menuItem2);
            contextMenu.Items.Add(menuItem3);

            return contextMenu;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string header = (sender as MenuItem).Header.ToString();

            Word word = buttonWordDictionary[buttonSender];
            int index = MainWindow.words.IndexOf(word);
            switch (header)
            {
                case "Edit word":
                    modify = new Modify(index);
                    modify.Show();
                    this.IsEnabled = false;
                    modify.Closing += (s, args) => ItemsScrollView(MainWindow.words);
                    modify.Closed += (s,args) => this.IsEnabled = true;
                    modify.Closing += (s, args) => this.Focus();
                    break;
                case "Delete word":
                    MainWindow.words.RemoveAt(index);
                    ItemsScrollView(MainWindow.words);
                    break;
                case "Delete counting":
                    MainWindow.words[index].Correct = 0;
                    MainWindow.words[index].Mistake = 0;
                    ItemsScrollView(MainWindow.words);
                    break;
            }
        }
    }
}
