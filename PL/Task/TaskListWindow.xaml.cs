using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
       
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        public Visibility IsButtonVisibile

        {
            get { return (Visibility)GetValue(IsButtonVisibileProperty); }
            set { SetValue(IsButtonVisibileProperty, value); }
        }

        public static readonly DependencyProperty IsButtonVisibileProperty =
            DependencyProperty.Register("IsButtonVisibile", typeof(Visibility), typeof(TaskListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience Complexity { get; set; } = BO.EngineerExperience.None;
        public BO.Status StatusTask { get; set; } = BO.Status.None;
        public TaskListWindow()
        {

            if (s_bl.StartProject != DateTime.MinValue)
                IsButtonVisibile = Visibility.Hidden;
            else
                IsButtonVisibile = Visibility.Visible;
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }

      
        
        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
                new TaskWindow(0).ShowDialog();
                TaskList = s_bl.Task.ReadAll();
  
        }

    

        private void TaskList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
                BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
                if (task != null)
                {
                    TaskWindow taskWindow = new TaskWindow(task.Id);
                    taskWindow.ShowDialog();
                    TaskList = s_bl.Task.ReadAll();

                }
            
        }

  

        private void cbSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (StatusTask == BO.Status.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => s_bl.Task.Read(item.Id).Status == StatusTask)!;


            TaskList = (Complexity == BO.EngineerExperience.None) ?
                TaskList : TaskList.Where(item => item.Complexity == Complexity)!;

        }
    }
   


}

