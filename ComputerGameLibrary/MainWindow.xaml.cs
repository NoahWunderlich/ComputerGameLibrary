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
        void readList()
        {
            string[] readArr = File.ReadAllLines(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\all_games.csv");

            foreach (String readline in readArr.Skip(1))
            {
                var temp = readline.Replace("\"", "");
                string[] line = temp.Split(',');

                Game game = new Game();
                game.Name = line[0];
                game.Platform = line[1];
                game.ReleaseDate = line[2];
                game.ReleaseYear = line[3];
                game.MetaScore = Int32.Parse(line[line.Length - 2]);
                game.UserReview = line[line.Length-1];
                game.RawLine = temp;

                string summary = "";

                for(int i = 4; i< line.Length - 2; i++)
                {
                    summary += line[i];
                }
                game.Summary = summary;

                DataGrid.Items.Add(game);
            }

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

        }

        private void OnClickOwn(object sender, RoutedEventArgs e)
        {
            BtnLoadList.Visibility = Visibility.Visible;
            readOwnList(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\own_games.csv");

        }

        void readOwnList(string filepath)
        {


        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {

        }

        private void OnClickAddReview(object sender, RoutedEventArgs e)
        {

        }
    }
}
