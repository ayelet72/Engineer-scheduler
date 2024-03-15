using PL.Engineer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Task? CurrentTask

        {
            get { return (BO.Task?)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        public IEnumerable<BO.Task> Tasks
        {
            get { return (IEnumerable<BO.Task>)GetValue(TasksProperty); }
            set { SetValue(TasksProperty, value); }
        }

        public static readonly DependencyProperty TasksProperty =
            DependencyProperty.Register("Tasks", typeof(IEnumerable<BO.Task>), typeof(TaskWindow), new PropertyMetadata(null));

        public IEnumerable<BO.Engineer> Engineers
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineersProperty); }
            set { SetValue(EngineersProperty, value); }
        }

        public static readonly DependencyProperty EngineersProperty =
            DependencyProperty.Register("Engineers", typeof(IEnumerable<BO.Engineer>), typeof(TaskWindow), new PropertyMetadata(null));


        public TaskWindow(int id = 0)
        {
            
            InitializeComponent();
            Tasks = s_bl.Task.ReadAll();

            try
            {
                // if id isn't a deafult create a new task . else, find the exsit engineer with the same id (Read Method)

                CurrentTask = (id != 0) ? s_bl.Task.Read(id)! : new BO.Task() { Id = 0, Description = " ", Alias = " ", Complexity = BO.EngineerExperience.None, Remarks = " ", RequiredEffortTime = null, Engineer = null, Deliverables = " " };
            }
            catch (BO.BlDoesNotExistException ex)
            {
                CurrentTask = null;
                MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void btnAddUpdateClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {

                    int? id = s_bl.Task.Create(CurrentTask!);
                    MessageBox.Show($"Task {id} was successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            if ((sender as Button)!.Content.ToString() == "Update")
            {
                try
                {
                    s_bl.Task.Update(CurrentTask!);
                    MessageBox.Show($"Task {CurrentTask!.Id} was successfully Update!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
       



    }
    //public class EngineersCollection : IEnumerable
    //{
    //    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    //    static readonly IEnumerable<BO.Engineer> s_engineers = s_bl.Engineer.ReadAll();
    //    public IEnumerator GetEnumerator() => s_engineers.GetEnumerator();
    //}
}
    



