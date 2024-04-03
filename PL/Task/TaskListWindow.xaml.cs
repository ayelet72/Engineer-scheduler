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
        public TaskListWindow()
        {
            
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }

        public TaskListWindow(int id)
        {

            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll( item=> item.EngineerId == 0)!;
        }
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience Complexity { get; set; } = BO.EngineerExperience.None;
        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Complexity == BO.EngineerExperience.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Complexity == (DO.EngineerExperience)Complexity)!;
        }
        public BO.Status StatusTask { get; set; } = BO.Status.None;
        private void cbStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (StatusTask == BO.Status.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => s_bl.Task.Read(item.Id).Status== StatusTask)!;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            new TaskWindow().ShowDialog();
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

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
   


}

