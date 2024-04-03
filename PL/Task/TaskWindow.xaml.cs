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
        public int? IdEngineer

        {
            get { return (int?)GetValue(IdEngineerProperty); }
            set { SetValue(IdEngineerProperty, value); }
        }

        public static readonly DependencyProperty IdEngineerProperty =
            DependencyProperty.Register("IdEngineer", typeof(int), typeof(TaskWindow), new PropertyMetadata(null));

      
        public BO.Engineer? CurrentEngineer

        {
            get { return (BO.Engineer?)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(TaskWindow), new PropertyMetadata(null));


        public IEnumerable<BO.Engineer> Engineers
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineersProperty); }
            set { SetValue(EngineersProperty, value); }
        }

        public static readonly DependencyProperty EngineersProperty =
            DependencyProperty.Register("Engineers", typeof(IEnumerable<BO.Engineer>), typeof(TaskWindow), new PropertyMetadata(null));


        public TaskWindow(int id = 0 )
        {
            
            Engineers = s_bl.Engineer.ReadAll();
            DataContext = this;
            //CurrentEngineer = null; 

            try
            {
                // if id isn't a deafult create a new task . else, find the exsit engineer with the same id (Read Method)

                CurrentTask = (id != 0) ? s_bl.Task.Read(id)! : new BO.Task() { Id = 0, Description = " ", Alias = " ", Complexity = BO.EngineerExperience.None, Remarks = " ", RequiredEffortTime = null, Engineer = null, Deliverables = " " };
                CurrentEngineer= (CurrentTask.Engineer!=null) ? s_bl.Engineer.Read(CurrentTask.Engineer.Id)! : new BO.Engineer() { Id = 0, Name = " ", Email = " ", Level = BO.EngineerExperience.None };
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
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    if(CurrentEngineer!=null)
                    {
                        CurrentTask!.Engineer = new BO.EngineerInTask
                        {
                            Name = CurrentEngineer.Name,
                            Id = CurrentEngineer.Id
                        };
                    }
                
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

                    if (CurrentEngineer != null)
                    {
                        CurrentTask!.Engineer = new BO.EngineerInTask
                        {
                            Name = CurrentEngineer.Name,
                            Id = CurrentEngineer.Id
                        };
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (sender is ComboBox comboBox)
            {
                
                BO.Engineer? selectedEngineer = null;
                if (comboBox.SelectedItem != null)
                {
                    selectedEngineer = (BO.Engineer)comboBox.SelectedItem;
                }


                CurrentEngineer = selectedEngineer;
            }
        }



    }
    
   
}
    




