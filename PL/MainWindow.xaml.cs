using PL.Engineer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        { new EngineerListWindow().Show(); }
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

    }
}