using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerGameLibrary
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            readList();
            BtnLoadList.Visibility = Visibility.Hidden;
        }
        void LoadFilters()
        {
            List<string> genreList = new List<string>();
            List<string> platformList = new List<string>();

            foreach (Game game in DataGrid.Items)
            {
                if (!genreList.Contains(game.Genre))
                {
                    genreList.Add(game.Genre);
                }
                if (!platformList.Contains(game.Platform))
                {
                    platformList.Add(game.Platform);
                }
            }
            Platform.ItemsSource = platformList;
            Genre.ItemsSource = genreList;
            Platform.SelectedIndex = 0;
            Genre.SelectedIndex = 0;
        }
        void readList()
        {
            DataGrid.Items.Clear();
            string[] readArr = File.ReadAllLines(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\all_games.csv");

            foreach (String readline in readArr.Skip(1))
            {

                DataGrid.Items.Add(LineToGame(readline));
            }
            LoadFilters();

        }

        void readOwnList(string filepath)
        {
            DataGrid.Items.Clear();
            string[] readArr = File.ReadAllLines(filepath);

            for (int i = 0; i <= readArr.Length - 2; i += 2)
            {
                Game game = LineToGame(readArr[i]);
                string[] nextline = readArr[i+1].Split(';');
                game.OwnReview = nextline[0];
                game.OwnScore = Double.Parse(nextline[1]);
                DataGrid.Items.Add(game);
            }
            LoadFilters();

        }


        private Game LineToGame(string str)
        {
            var temp = str.Replace("\"", "");
            string[] line = temp.Split(',');

            Game game = new Game();
            game.Name = line[0];
            game.Platform = line[1];
            game.ReleaseDate = line[2];
            game.ReleaseYear = line[3];
            game.MetaScore = Int32.Parse(line[line.Length - 2]);
            game.UserReview = line[line.Length - 1];
            game.RawLine = temp;

            string summary = "";

            for (int i = 4; i < line.Length - 2; i++)
            {
                summary += line[i];
            }
            game.Summary = summary;

            return game;
        }
        private void OnFileClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog(this) == true)
            {
                readOwnList(dialog.FileName);
            }
            else
            {
                readOwnList(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\own_games.csv");
            }
            // für alle fälle
            

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Game game = (Game)DataGrid.SelectedItem;
            LabelName.Content = game.Name;
            TextBlockSummary.Text = game.Summary;
            LabelDate.Content = game.ReleaseDate + " " + game.ReleaseYear;
        }

        public void OnClickAll(object sender, RoutedEventArgs e)
        {
            readList();
        }

        private void OnClickOwn(object sender, RoutedEventArgs e)
        {
            BtnLoadList.Visibility = Visibility.Visible;
            readOwnList(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\own_games.csv");

        }

      
        private void ApplyFilter(object sender, RoutedEventArgs e)
        {

        }

        private void OnClickAddReview(object sender, RoutedEventArgs e)
        {
            Game game = (Game)DataGrid.SelectedItem;
            //game.RawLine += "," + OwnReview.Text;
            string[] arr = new string[2];

            arr[0] = game.RawLine;
            arr[1] = OwnReview.Text;
            File.AppendAllLines(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\own_games.csv", arr);

        }

        private void OnTextBoxContains(object sender, TextChangedEventArgs e)
        {
            readList();
            //TextBoxContains.Text;
            foreach (Game game in DataGrid.Items)
            {
                if (!game.RawLine.Contains(TextBoxContains.Text))
                {
                    DataGrid.Items.Remove(game);
                }
            }

            }
    }
}
