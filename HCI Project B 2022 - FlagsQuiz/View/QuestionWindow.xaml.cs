using HCI_Project_B_2022___FlagsQuiz.Data.DataAccess.MySQLDAO;
using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HCI_Project_B_2022___FlagsQuiz.View
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private readonly Player player;
        private readonly Country country;
        private readonly int number;
        private readonly List<Country> flags;
        private readonly Game game;
        private readonly Button[] buttons;
        private readonly Stopwatch stopwatch;
        private readonly List<(string, string)> answers;
        internal QuestionWindow(Player player, int number, List<Country> flags, Game game, Stopwatch stopwatch, List<(string, string)> answers)
        {
            if (number != 1)
                stopwatch.Stop();
            InitializeComponent();
            this.player = player;
            this.number = number;
            this.flags = flags;
            this.game = game;
            this.stopwatch = stopwatch;
            this.answers = answers;
            buttons = new Button[] { btnA1, btnA2, btnA3, btnA4 };
            country = flags[number - 1];
            lbTitle.Text += number + " / " + game.NumberOfQuestions;
            imgFlag.Source = new BitmapImage(new Uri(country.Flags.Png, UriKind.Absolute));
            if (game.Difficulty == Difficulty.Medium)
                InitMedium();
            else if (game.Difficulty == Difficulty.Hard)
                InitHard();
            stopwatch.Start();
        }

        private void InitMedium()
        {
            int rnd = new Random().Next(4);
            List<Country> temp = flags.Where(f => f.Name != country.Name).OrderBy(f => Guid.NewGuid()).ToList();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == rnd)
                    buttons[i].Content = country.Name;
                else
                    buttons[i].Content = temp[i].Name;
            }
        }

        private void InitHard()
        {
            foreach (var button in buttons)
                button.Visibility = Visibility.Hidden;
            tbAnswer.Visibility = Visibility.Visible;
            btnNextHard.Visibility = Visibility.Visible;
        }

        private void Click_Next_Medium(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CheckAnswer((string)button.Content);
            NextQuestion();
        }

        private void CheckAnswer(string answer)
        {
            if (answer == country.Name)
                game.NumberOfCorrectAnswers++;
            answers.Add((country.Name, answer));
        }
        private void NextQuestion()
        {
            if (number != game.NumberOfQuestions)
            {
                new QuestionWindow(player, number + 1, flags, game, stopwatch, answers).Show();
                Close();
            }
            else
            {
                stopwatch.Stop();
                game.ElapsedTime = stopwatch.ElapsedMilliseconds;
                game.Player = player;
                if (player != null)
                {
                    try
                    {
                        new MySQLGameDAO().Add(game);
                    }
                    catch (Exception) { }
                }
                new ResultWindow(player, game, stopwatch, answers).Show();
                Close();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (player == null)
                new MainWindow(game).Show();
            else
                new MainWindow(player, game).Show();
            Close();
        }

        private void BtnNextHard_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(tbAnswer.Text);
            NextQuestion();
        }

        private void TbAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckAnswer(tbAnswer.Text);
                NextQuestion();
            }
        }
    }
}
