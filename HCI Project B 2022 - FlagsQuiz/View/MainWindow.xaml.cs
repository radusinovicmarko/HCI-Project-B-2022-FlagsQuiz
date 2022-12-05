using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using HCI_Project_B_2022___FlagsQuiz.Service;
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
using System.Security.Cryptography;
using System.Diagnostics;

namespace HCI_Project_B_2022___FlagsQuiz.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Player player;
        private readonly Game newGame = new Game()
        {
            Difficulty = Difficulty.Medium,
            NumberOfQuestions = 25
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        internal MainWindow(Player player)
        {
            InitializeComponent();
            this.player = player;
            btnNewAnonymousGame.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Collapsed;
            btnRegister.Visibility = Visibility.Collapsed;
            btnNewGame.Visibility = Visibility.Visible;
            btnHistory.Visibility = Visibility.Visible;
            btnLogout.Visibility = Visibility.Visible;
            btnOptions.SetValue(Grid.RowProperty, 4);
            btnRanking.SetValue(Grid.RowProperty, 5);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow(this).Show();
            Hide();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow(this).Show();
            Hide();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            new OptionsWindow(this, newGame).Show();
            Hide();
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            List<Country> flags = new CountryService().GetAll().OrderBy(f => Guid.NewGuid()).ToList();
            new QuestionWindow(player, 1, flags, newGame, new Stopwatch(), new List<(string, string)>()).Show();
            Close();
        }
    }
}
