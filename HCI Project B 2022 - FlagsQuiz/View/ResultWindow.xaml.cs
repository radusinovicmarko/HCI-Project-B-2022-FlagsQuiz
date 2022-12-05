using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private readonly Player player;
        internal ResultWindow(Player player, Game game, Stopwatch stopwatch, List<(string, string)> answers)
        {
            InitializeComponent();
            this.player = player;
            textBlockScore.Text = game.NumberOfCorrectAnswers + "/" + game.NumberOfQuestions + " questions correctly answered!";
            textBlockTime.Text += stopwatch.Elapsed.TotalSeconds + " seconds";
            int i = 1;
            foreach (var item in answers)
                listAnswers.Items.Add(i++ + ". Your answer: " + item.Item2 + " | Correct answer: " + item.Item1);
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            if (player == null)
                new MainWindow().Show();
            else
                new MainWindow(player).Show();
            Close();
        }
    }
}
