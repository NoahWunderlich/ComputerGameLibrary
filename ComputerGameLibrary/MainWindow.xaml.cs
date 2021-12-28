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
        string AllListSource = @"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\all_games.csv";
        string OwnListSource = @"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\own_games.csv";
        public MainWindow()
        {
            InitializeComponent();
            readList();
        }
        void LoadFilters()
        {
            List<string> genreList = new List<string>();
            List<string> platformList = new List<string>();
            platformList.Add("");
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
            DataGridPersonalScore.Visibility = Visibility.Collapsed;
            AddGamePanel.Visibility = Visibility.Visible;
            OwnListOptions.Visibility = Visibility.Collapsed;
            AllListOptions.Visibility = Visibility.Visible;


            DataGrid.Items.Clear();
            string[] readArr = File.ReadAllLines(AllListSource);

            foreach (String readline in readArr.Skip(1))
            {

                DataGrid.Items.Add(LineToGame(readline));
            }
            LoadFilters();

        }

        void readOwnList(string filepath)
        {
            DataGridPersonalScore.Visibility = Visibility.Visible;
            AllListOptions.Visibility = Visibility.Collapsed;
            AddGamePanel.Visibility = Visibility.Hidden;
            OwnListOptions.Visibility = Visibility.Visible;

            DataGrid.Items.Clear();
            string[] readArr = File.ReadAllLines(filepath);

            for (int i = 0; i <= readArr.Length - 2; i += 2)
            {
                Game game = LineToGame(readArr[i]);
                string[] nextline = readArr[i+1].Split(',');

                game.OwnScore = int.Parse(nextline[nextline.Length-1]);

                for (int j = 0; j<nextline.Length-1; j++)
                {
                    game.OwnReview += nextline[j];
                }


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
        private void OnFileImportClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog(this) == true)
            {
                readOwnList(dialog.FileName);
            }
            else
            {
                readOwnList(OwnListSource);
            }
            // für alle fälle
            

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {

                Game game = (Game)DataGrid.SelectedItem;
                LabelName.Content = game.Name;
                TextBlockSummary.Text = game.Summary;
                LabelDate.Content = game.ReleaseDate + " " + game.ReleaseYear;

                if (game.OwnReview != null)
                {
                    TextBlockPersonalReview.Text = game.OwnReview;
                }
            }
        }

        public void OnClickAll(object sender, RoutedEventArgs e)
        {
            readList();
            
        }

        private void OnClickOwn(object sender, RoutedEventArgs e)
        {
            DataGrid.SelectedItem = null;
            readOwnList(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\own_games.csv");

        }

      
        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            readList();

            List<Game> removeGames = new List<Game>();
            foreach (Game game in DataGrid.Items)
            {
                if (!game.Summary.Contains(TextBoxContains.Text.ToString()))
                {
                    removeGames.Add(game);
                }
            }
            foreach(Game g in removeGames)
            {
                DataGrid.Items.Remove(g);
            }

        
    }

        private void OnClickAddReview(object sender, RoutedEventArgs e)
        {

        }

        private void OnTextBoxContains(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game();

            game.Name = NewGameName.Text;
            game.Genre = NewGameGenre.Text;
            game.Platform = NewGamePlatform.Text ;
            game.ReleaseDate = NewGameRelease.Text ;
            game.ReleaseYear = NewGameReleaseYear.Text;
            game.MetaScore = Int32.Parse(NewGameMetaScore.Text);
            game.UserReview = NewGameUserReview.Text;
            game.Summary = NewGameSummary.Text;
            game.RawLine = game.Name +","+ game.Platform +"," + game.ReleaseDate +"," + game.Summary +"," +  game.MetaScore+"," +game.UserReview ; //name,platform,release_date,summary,meta_score,user_review

            File.AppendAllText(AllListSource, game.RawLine);
        }

        private void OnClickAddToOwn(object sender, RoutedEventArgs e)
        {
            Game game = (Game)DataGrid.SelectedItem;
            string[] arr = new string[2];
            if (Int32.Parse(TextBoxOwnScore.Text) <= 100 && Int32.Parse(TextBoxOwnScore.Text) >= 0)
            {
                arr[0] = game.RawLine;
                arr[1] = TextBoxOwnReview.Text + "," + TextBoxOwnScore.Text;
                File.AppendAllLines(AllListSource, arr);
            }

        }

        private void OnFileExportClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.Title = "Export your personal list";
            //System.IO.FileStream stream = (System.IO.FileStream)dialog.OpenFile();

            if (dialog.ShowDialog() == DialogResult)
            {
                string newDirectory = dialog.FileName;
                System.IO.File.Copy(OwnListSource, newDirectory);
            }

        }
    }
}
