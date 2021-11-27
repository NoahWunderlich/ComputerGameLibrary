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
            string[] readArr = File.ReadAllLines(@"C:\Users\NOAH-WUNDERLICH\source\repos\ComputerGameLibrary\ComputerGameLibrary\video_games.csv");

            foreach (String readline in readArr.Skip(1))
            {
                var temp = readline.Replace("\"", "");
                string[] line = temp.Split(',');
                testbox1.Text = line[0];
                testbox2.Text = line.Length.ToString();
                testbox3.Text = line[line.Length - 29]; // genre
                testbox4.Text = line.Length.ToString();

                
                Game game = new Game();

                if (line.Length < 36)
                {
                    string[] arr = new string[line.Length - 36];
                    for(int i = 0; i< line.Length-36; i++)
                    {
                        arr[i] = line[6+i];
                    }
                    game.Genres = arr;
                }

                game.Name = line[0];
                game.Platform = line[line.Length - 24];
                game.Publisher = line[line.Length - 26];

                ListBoxAll.Items.Add(game);
            }

        }
    }
}
