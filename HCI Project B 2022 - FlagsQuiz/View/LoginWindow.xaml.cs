using HCI_Project_B_2022___FlagsQuiz.Data.DataAccess.MySQLDAO;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly Window previousWindow;
        public LoginWindow(Window previousWindow)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(tbUsername.Text) || string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("Credentials missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var loginInfo = MySQLUtils.Login(tbUsername.Text, pbPassword.Password);
                if (loginInfo.Item1)
                {
                    Player player = new MySQLPlayerDAO().Get(new Player() { PlayerId = loginInfo.Item2 })[0];
                    new MainWindow(player).Show();
                    Close();
                    previousWindow.Close();
                } 
                else
                    MessageBox.Show("Login unsuccessful.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
