using PL.Engineer;
using PL.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public static readonly DependencyProperty TaskListProperty =
           DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }


        //private DateTime _projectStartDate;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public DateTime ProjectStartDate
        //{
        //    get { return _projectStartDate; }
        //    set
        //    {
        //        if (_projectStartDate != value)
        //        {
        //            _projectStartDate = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public AdminWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
           // DataContext = this;
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
                MessageBox.Show($"initlization was successfully done", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
        private void btnResetEngineers_Click(object sender, RoutedEventArgs e)
        {
            s_bl.ResetDB();
            MessageBox.Show($"Reset was successfully done", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        private void btnGant_Click(object sender, RoutedEventArgs e)
        {

        }

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private void CreateSchedule_Click(object sender, RoutedEventArgs e)
        { 
           // s_bl.CreateAutomateSchedule(TaskList, ProjectStartDate);
            
        }
        //private void DatePicker_SelectedDate(object sender, SelectionChangedEventArgs e)
        //{

        //}



    }
}

