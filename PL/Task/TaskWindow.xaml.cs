using BO;
using DO;
using PL.Engineer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public class SelectTask
        {

            public bool IsSelected { get; set; }
            public BO.Task? task { get; set; }
        }

        public List<SelectTask>? UpDependencies
        {
            get { return (List<SelectTask>?)GetValue(UpDependenciesProperty); }
            set { SetValue(UpDependenciesProperty, value); }
        }
        public static readonly DependencyProperty UpDependenciesProperty =
         DependencyProperty.Register("UpDependencies", typeof(List<SelectTask>), typeof(TaskWindow));


        public static readonly DependencyProperty EnableProperty =
      DependencyProperty.Register(" Enable", typeof(bool), typeof(TaskWindow), new PropertyMetadata(null));

        public bool Enable
        {
            get { return (bool)GetValue(EnableProperty); }
            set { SetValue(EnableProperty, value); }
        }

        public IEnumerable<BO.Task> Tasks
        {
            get { return (IEnumerable<BO.Task>)GetValue(TasksProperty); }
            set { SetValue(TasksProperty, value); }
        }

        public static readonly DependencyProperty TasksProperty =
            DependencyProperty.Register("Tasks", typeof(IEnumerable<BO.Task>), typeof(TaskWindow), new PropertyMetadata(null));

        public BO.Task? CurrentTask

        {
            get { return (BO.Task?)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        public static readonly DependencyProperty CurrentTaskProperty =
          DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        public BO.Engineer? SelectedEngineer
        {
            get { return (BO.Engineer?)GetValue(SelectedEngineerProperty); }
            set { SetValue(SelectedEngineerProperty, value); }
        }
        public static readonly DependencyProperty SelectedEngineerProperty =
         DependencyProperty.Register("SelectedEngineer", typeof(BO.Engineer), typeof(TaskWindow));

        public TaskWindow(int id = 0 )
        {
           List<BO.Task> temp = s_bl.Task.ReadAll().ToList();
            UpDependencies = temp.Select(item => new SelectTask{ task = item, IsSelected=false }).ToList();
            SelectedEngineer = null;
            Enable = true;

            try
            {
                // if id isn't a deafult create a new task . else, find the exsit engineer with the same id (Read Method)

                CurrentTask = (id != 0) ? s_bl.Task.Read(id)! : new BO.Task() { Id = 0, Description = " ", Alias = " ", Complexity = BO.EngineerExperience.None, Remarks = " ", RequiredEffortTime = null, Engineer = null, Deliverables = " " };
                //SelectedEngineer= (CurrentTask.Engineer!=null) ? s_bl.Engineer.Read(CurrentTask.Engineer.Id)! : new BO.Engineer() { Id = 0, Name = " ", Email = " ", Level = BO.EngineerExperience.None };
                if (id != 0 && CurrentTask.Dependencies!=null)
                {
                    foreach (var dependency in CurrentTask.Dependencies)
                    {
                        var selectTask = UpDependencies.FirstOrDefault(x => x.task!.Id == dependency.Id);
                        if (selectTask != null)
                        {
                            selectTask.IsSelected = true;
                        }
                    }
                }
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

            InitializeComponent();
        }
        public TaskWindow(int id = 0, BO.Engineer? engineer=null)
        {

            SelectedEngineer = engineer!;
            Enable = false;
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

            InitializeComponent();
        }
        private void btnAddUpdateClick(object sender, RoutedEventArgs e)
        {
            if (SelectedEngineer == null)
            {
                CurrentTask!.Dependencies?.Clear();
                CurrentTask!.Dependencies = UpDependencies!
                .Where(item => item.IsSelected == true)
                .Select(item => new TaskInList
                {
                    Id = item.task!.Id,
                    Alias = s_bl.Task.Read(item.task.Id).Alias,
                    Description = s_bl.Task.Read(item.task.Id).Description,
                    Status = s_bl.Task.Read(item.task.Id).Status
                })
                .ToList();
            }
            
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
                    if (SelectedEngineer != null)
                    {
                        CurrentTask!.Engineer = new BO.EngineerInTask { Id = SelectedEngineer.Id, Name = SelectedEngineer.Name };
                        CurrentTask!.StartDate = s_bl.Clock;
                        SelectedEngineer.Task = new BO.TaskInEngineer { Id = CurrentTask.Id, Alias = CurrentTask.Alias };
                        s_bl.Engineer.Update(SelectedEngineer);
                    }
                        
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem != null)
                {
                    SelectedEngineer = (BO.Engineer)comboBox.SelectedItem;
                }
 
            }
        }
     



    }
    
   
}
    




