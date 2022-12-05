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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly Window previousWindow;
        public RegisterWindow(Window previousWindow)
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

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void Register()
        {
            if (string.IsNullOrEmpty(tbUsername.Text) || string.IsNullOrEmpty(pbPassword.Password) || string.IsNullOrEmpty(pbPasswordAgain.Password))
            {
                MessageBox.Show("Credentials missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pbPassword.Password != pbPasswordAgain.Password)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Player player = new Player()
            {
                Username = tbUsername.Text,
                Password = pbPassword.Password
            };
            try
            {
                new MySQLPlayerDAO().Add(player);
                MessageBox.Show("Registration successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured or maybe the username is taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Register();
        }
    }
}
