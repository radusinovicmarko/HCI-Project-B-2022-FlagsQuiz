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

namespace HCI_Project_B_2022___FlagsQuiz.View
{
    /// <summary>
    /// Interaction logic for AllFlagsWindow.xaml
    /// </summary>
    public partial class AllFlagsWindow : Window
    {
        private readonly Window previousWindow;
        internal AllFlagsWindow(Window previousWindow, List<Country> allFlags)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
            DataContext = new { Items = allFlags };
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
}
