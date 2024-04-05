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
        private string _currentTaskMessage = "There is no current task assigned to this engineer.";

        public bool IsButtonVisibile

        {
            get { return (bool)GetValue(IsButtonVisibileProperty); }
            set { SetValue(IsButtonVisibileProperty, value); }
        }

        public static readonly DependencyProperty IsButtonVisibileProperty =
            DependencyProperty.Register("IsButtonVisibile", typeof(bool), typeof(EngineerViewWindow), new PropertyMetadata(null));

        public string CurrentTaskMessage
        {
            get => CurrentTask == null ? _currentTaskMessage : string.Empty;
        }
        public BO.Engineer? CurrentEngineer

        {
            get { return (BO.Engineer?)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        public BO.Task? CurrentTask

        {
            get { return (BO.Task?)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(EngineerWindow), new PropertyMetadata(null));
        public EngineerViewWindow(int id)
        {
            
            CurrentEngineer= s_bl.Engineer.Read(id)!;

            InitializeComponent();
            DataContext = this;

            //if engineer have a task
            if(CurrentEngineer.Task!=null)
            {
                CurrentTask = s_bl.Task.Read(CurrentEngineer.Task.Id);
                IsButtonVisibile = false;
            }
           
            else
            {
                CurrentTask = null;
                IsButtonVisibile = true;

            }

        }

        private void FinishTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentTask!.CompleteDate = DateTime.Now;
            s_bl.Task.Update(CurrentTask);

        }

        private void ShowTaskListButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private void AssignTask_Click(object sender, RoutedEventArgs e)
        {
            // opening a new window for Assigning an engineer to the task

            TaskAssignmentWindow addTaskWindow = new TaskAssignmentWindow(CurrentEngineer!.Id);
            addTaskWindow.ShowDialog();
            Close();


        }
    }
}
