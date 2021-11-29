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
                game.UserReview = Double.Parse(line[line.Length-1]);


                DataGridAll.Items.Add(game);
            }

        }

        private void OnClickAllDataButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
