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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            new TaskWindow().ShowDialog();
            TaskList = s_bl.Task.ReadAll();
        }

        private void EngineerList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? task = (sender as ListView)?.SelectedItem as BO.Engineer;
            if (task != null)
            {
                EngineerWindow taskWindow = new EngineerWindow(task.Id);
                taskWindow.ShowDialog();
                TaskList = s_bl.Task.ReadAll();

            }
        }

        private void TaskList_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
   


}

