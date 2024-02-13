using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LanguageAppWpf
{
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
                            Correct.IsChecked = false;

                        return (actualList.OrderBy(y => y.Correct).ToList(),false);
                    }
                    else if(x.ToLower() == "uncorrect")
                    {
                        if(Correct.IsChecked == true)
                            Uncorrect.IsChecked = false;

                        return (actualList.OrderBy(y => y.Mistake).ToList(),false);
                    }
                    else if (x.ToLower() == "alfabetical" && Alfabetical.IsChecked == true)
                        return (actualList.OrderBy(y => y.WordName).ToList(),false);
                    else if (x.ToLower() == "alfabetical" && Alfabetical.IsChecked == false)
                        return (actualList,false);
                }
            }

            foreach (Word word in words)
            {
                if (word.Category.ToString().ToLower() == x.ToLower())
                    wordsToNewList.Add(word);
            };  
            return (wordsToNewList, true);

        } // List of words to methods
        private void Checked(object sender, RoutedEventArgs e)
        {
            var items = ListOfWordsToMethods(sender);
            if (items.Item2 == true)
            {
                actualList = actualList.Union(items.Item1).ToList();
                ExceptionsWithSort();
            }
            else
                actualList = items.Item1;

            ItemsScrollView(actualList);
        } // Checked
        private void Unchecked(object sender, RoutedEventArgs e)
        {
            var items = ListOfWordsToMethods(sender);
            if (items.Item2 == true)
            {
                actualList = actualList.Except(items.Item1).ToList();
                ExceptionsWithSort();
            }
            else
                actualList = items.Item1;

            ItemsScrollView(actualList);
        } // Unchecked
        private void ExceptionsWithSort()
        {
            if (Alfabetical.IsChecked == true)
                actualList = actualList.OrderBy(y => y.WordName).ToList();
            else if (Correct.IsChecked == true)
                actualList = actualList.OrderBy(y => y.Correct).ToList();
            else if (Uncorrect.IsChecked == true)
                actualList = actualList.OrderBy(y => y.Mistake).ToList();
        } // Exceptions with sort
        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Exit button
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
        } // Load scroll view
        private void Modify(object sender, RoutedEventArgs e)
        {
            buttonSender = sender as Button;
            myContextMenu.IsOpen = true;
        } // Modify button
        private void ItemsScrollView(List<Word> list)
        {
            int count = 0, height = 30;
            buttonWordDictionary.Clear();
            if (ScrollList != null)
            {
                ScrollList.ClearValue(ScrollViewer.ContentProperty);
            }

            StackPanel panel = new StackPanel();
            StackPanel stackTheme = new StackPanel();
            stackTheme.Orientation = Orientation.Horizontal;
            for (int i = 0; i < 5; i++)
            {
                int width = 0;

                if (i == 0 || i == 1)     
                    width = 290;
                else if (i == 2)
                    width = 130;
                else
                    width = 100;

                TextBlock textBlock = new TextBlock()
                {
                    Text = i == 0 ? "Word" : i == 1 ? "Translation" : i == 2 ? "Category" : i == 3 ? "Mistake" : "Correct",
                    Width = width,
                    Height = height,
                    TextAlignment = TextAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White
                };
                stackTheme.Children.Add(textBlock);
            }
            panel.Children.Add(stackTheme);
            foreach (Word word in list)
            {
                if (word != null)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    for(int i = 0; i < 5; i++)
                    {
                        int width = 0;

                        if(i == 0 || i == 1)
                            width = 290;
                        else if(i == 2)
                            width = 130;
                        else
                            width = 100;

                        TextBlock textBlock = new TextBlock()
                        {
                            Text = i == 0 ? word.WordName : i == 1 ? word.Translation.ToString() : i == 2 ? word.Category.ToString() : i == 3 ? word.Mistake.ToString() : word.Correct.ToString(),
                            Width = width,
                            Height = height,
                            TextAlignment = TextAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontSize = 20,
                            FontWeight = FontWeights.Bold,
                            Foreground = Brushes.White
                        };
                        stackPanel.Children.Add(textBlock);
                    }

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("pack://application:,,,/LanguageAppWpf;component/Resources/iconList.png");
                    bitmap.EndInit();

                    Image image = new Image()
                    {
                        Source = bitmap,
                        Stretch = Stretch.Fill
                    };

                    Button button = new Button()
                    {
                        Content = image,
                        Width = 30,
                        Height = 30,
                        Margin = new Thickness(20, 10, 20, 10),
                        Name = "iconList" + count,
                        Background = Brushes.Transparent,
                        BorderBrush = Brushes.Transparent,
                        Foreground = Brushes.Transparent,
                        IsEnabled = true
                    };

                    buttonWordDictionary.Add(button, word);
                    button.Click += Modify;
                    myContextMenu = CreateContextMenu();
                    button.ContextMenu = myContextMenu;

                    stackPanel.Children.Add(button);
                    panel.Children.Add(stackPanel);

                }
                ScrollList.Content = panel;
            }
        } // Items scroll view
        private ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
           
            for (int i = 0; i < 3; i++)
            {
                MenuItem menuItem = new MenuItem()
                {
                    Header = i == 0 ? "Edit word" : i == 1 ? "Delete word" : "Delete counting"
                };
                menuItem.Click += MenuItem_Click;
                contextMenu.Items.Add(menuItem);
            }
            return contextMenu;
        } // Create context menu
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
                    modify.Closing += (s, args) =>
                    {
                        ItemsScrollView(MainWindow.words);
                        Focus();
                    };
                    modify.Closed += (s,args) => this.IsEnabled = true;
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
        }  // Context menu items
    }
}
