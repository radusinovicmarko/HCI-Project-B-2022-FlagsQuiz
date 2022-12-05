using HCI_Project_B_2022___FlagsQuiz.Data.Model;
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

namespace HCI_Project_B_2022___FlagsQuiz.View
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private readonly Window previousWindow;
        private readonly Game game;
        internal OptionsWindow(Window previousWindow, Game game)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
            this.game = game;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (rbMedium.IsChecked.Value)
                game.Difficulty = Difficulty.Medium;
            else if (rbHard.IsChecked.Value)
                game.Difficulty = Difficulty.Hard;
            if (rb10.IsChecked.Value)
                game.NumberOfQuestions = 10;
            else if (rb25.IsChecked.Value)
                game.NumberOfQuestions = 25;
            else if (rb50.IsChecked.Value)
                game.NumberOfCorrectAnswers = 50;
            else if (rb100.IsChecked.Value)
                game.NumberOfQuestions = 100;
            previousWindow.Show();
            Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
