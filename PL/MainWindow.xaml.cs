using BlApi;
using PL.Engineer;
using PL.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly IBl s_bl = Factory.Get();

        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(DateTime.Now));

        // Property to access the CurrentTime dependency property
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();

            // Update the current time periodically
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += (sender, e) => CurrentTime = DateTime.Now;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
       
     
        
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EnteringIDWindow().ShowDialog();
        } 
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        { new AdminWindow().Show(); }


        // Button click handlers:
        private void AdvanceHour_Click(object sender, RoutedEventArgs e) => s_bl.AdvanceTimeByHour(1);

        private void AdvanceDay_Click(object sender, RoutedEventArgs e) => s_bl.AdvanceTimeByDay(1);
        private void AdvanceMonth_Click(object sender, RoutedEventArgs e) => s_bl.AdvanceTimeByMonth(1);

        private void AdvanceYear_Click(object sender, RoutedEventArgs e) => s_bl.AdvanceTimeByYear(1);

        private void ResetClock_Click(object sender, RoutedEventArgs e) => s_bl.InitializeTime();
    }
}
