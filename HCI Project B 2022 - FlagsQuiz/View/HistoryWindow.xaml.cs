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
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private readonly Window previousWindow;
        internal HistoryWindow(Window previousWindow, Player player)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
            try
            {
                this.DataContext = new
                {
                    Items = new MySQLGameDAO().Get(new Game() { Player = player })
                };
            } 
            catch (Exception)
            {
                MessageBox.Show("An error has occured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                previousWindow.Show();
                Close();
            }
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
    }

    internal class CustomBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)(1.0 * ((long)value) / 1000);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
