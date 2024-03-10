using PL.Engineer;
using PL.Task;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public AdminWindow()
        {
            InitializeComponent();
        }
        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        { new EngineerListWindow().Show(); }
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        { new TaskListWindow().Show(); }
        private void btnInitEngineers_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to init data?", "Yes", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the answer if the user
            if (result == MessageBoxResult.Yes)
            {
                s_bl.InitializeDB();
            }

        }
        private void btnResetEngineers_Click(object sender, RoutedEventArgs e)
        {
            s_bl.ResetDB();
        }

        private void btnGant_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

