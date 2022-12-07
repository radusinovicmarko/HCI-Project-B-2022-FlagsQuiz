using HCI_Project_B_2022___FlagsQuiz.Data.DataAccess.MySQLDAO;
using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace HCI_Project_B_2022___FlagsQuiz.View
{
    /// <summary>
    /// Interaction logic for RankingsWindow.xaml
    /// </summary>
    public partial class RankingsWindow : Window
    {
        private readonly Window previousWindow;
        public RankingsWindow(Window previousWindow)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
            LoadRankings();
        }

        private void LoadRankings()
        {
            var list = new MySQLGameDAO().Get(GetRankingFilters());
            List<RankedGame> rankedList = new List<RankedGame>();
            int i = 1;
            list.ForEach(g => rankedList.Add(new RankedGame()
            {
                GameId = g.GameId,
                NumberOfCorrectAnswers = g.NumberOfCorrectAnswers,
                NumberOfQuestions = g.NumberOfQuestions,
                ElapsedTime = g.ElapsedTime,
                Difficulty = g.Difficulty,
                Player = g.Player,
                Rank = i++
            }));
            DataContext = new { Items = rankedList };
        }

        private Game GetRankingFilters()
        {
            Game game = new Game();
            if (rbMedium.IsChecked.Value)
                game.Difficulty = Difficulty.Medium;
            else if (rbHard.IsChecked.Value)
                game.Difficulty = Difficulty.Hard;
            if (rb10.IsChecked.Value)
                game.NumberOfQuestions = 10;
            else if (rb25.IsChecked.Value)
                game.NumberOfQuestions = 25;
            else if (rb50.IsChecked.Value)
                game.NumberOfQuestions = 50;
            return game;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Rb_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadRankings();
            } catch (Exception)
            {
                MessageBox.Show("An error has occured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    internal class RankedGame : Game
    {
        public int Rank { get; set; } = 1;
    }
}
