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
    /// Interaction logic for EngineerViewWindow.xaml
    /// </summary>
    public partial class EngineerViewWindow : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public Visibility IsButtonVisibile

        {
            get { return (Visibility)GetValue(IsButtonVisibileProperty); }
            set { SetValue(IsButtonVisibileProperty, value); }
        }

        public static readonly DependencyProperty IsButtonVisibileProperty =
            DependencyProperty.Register("IsButtonVisibile", typeof(Visibility), typeof(EngineerViewWindow), new PropertyMetadata(null));
        public Visibility IsButtonDoneVisibile

        {
            get { return (Visibility)GetValue(IsButtonDoneVisibileProperty); }
            set { SetValue(IsButtonDoneVisibileProperty, value); }
        }

        public static readonly DependencyProperty IsButtonDoneVisibileProperty =
            DependencyProperty.Register("IsButtonDoneVisibile", typeof(Visibility), typeof(EngineerViewWindow), new PropertyMetadata(null));

        public BO.Engineer? CurrentEngineer

        {
            get { return (BO.Engineer?)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerViewWindow), new PropertyMetadata(null));
        public BO.Task? CurrentTask

        {
            get { return (BO.Task?)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(EngineerViewWindow), new PropertyMetadata(null));
        public EngineerViewWindow(int id)
        {
            CurrentEngineer = s_bl.Engineer.Read(id)!;
            CurrentTask = null;
            IsButtonVisibile = Visibility.Hidden;

            //if engineer have a task
            if (CurrentEngineer.Task != null)
            {
                CurrentTask = s_bl.Task.Read(CurrentEngineer.Task.Id);
                IsButtonVisibile = Visibility.Hidden;
                IsButtonDoneVisibile = Visibility.Visible;
            }

            else
            {
                CurrentTask = null;
                IsButtonVisibile = Visibility.Visible;
                IsButtonDoneVisibile = Visibility.Hidden;

            }
            InitializeComponent();
        }




        private void AssignTask_Click(object sender, RoutedEventArgs e)
        {
            // opening a new window for Assigning an engineer to the task

            try
            {
                s_bl.IsSchedule();
                TaskAssignmentWindow addTaskWindow = new TaskAssignmentWindow(CurrentEngineer!.Id);
                addTaskWindow.ShowDialog();
                Close();

            }
            catch (BO.BlIsScheduled ex)
            {


                MessageBox.Show(ex.Message, "project is already scheduled", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }

        }

        private void DoneTask_Click(object sender, RoutedEventArgs e)
        {
            CurrentTask!.CompleteDate = s_bl.Clock;
            s_bl.Task.Update(CurrentTask);
            CurrentEngineer!.Task = null;
            s_bl.Engineer.Update(CurrentEngineer);
            IsButtonDoneVisibile = Visibility.Hidden;
            this.Close();

        }
    }
}
