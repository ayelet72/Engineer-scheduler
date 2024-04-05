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
    /// Interaction logic for TaskAssignmentWindow.xaml
    /// </summary>
    public partial class TaskAssignmentWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Engineer? CurrentEngineer

        {
            get { return (BO.Engineer?)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(TaskAssignmentWindow), new PropertyMetadata(null));
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskAssignmentWindow), new PropertyMetadata(null));
        public TaskAssignmentWindow()
        {
            InitializeComponent();
        }
        public TaskAssignmentWindow(int id=0)
        {
            CurrentEngineer = s_bl.Engineer.Read(id);
            //TaskList= s_bl.Task.ReadAll(item => (item.EngineerId==null && C ))

            InitializeComponent();
        }
    }
}
