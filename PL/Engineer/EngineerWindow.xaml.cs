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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
       
        public BO.Engineer? CurrentEngineer

        {
            get { return (BO.Engineer?)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        
        public  EngineerWindow(int id = 0)
        {
            InitializeComponent();
            try
            {
                // if id isn't a deafult create a new engineer . else, find the exsit engineer with the same id (Read Method)

                CurrentEngineer = (id != 0) ? s_bl.Engineer.Read(id)! : new BO.Engineer() { Id = 0, Name = " ", Email = " ", Level = BO.EngineerExperience.None };
            }
            catch(BO.BlDoesNotExistException ex)
            {
                CurrentEngineer = null;
                MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button)!.Content.ToString()=="Add")
            {
                try
                {

                    int? id = s_bl.Engineer.Create(CurrentEngineer!);
                    MessageBox.Show($"Engineer {id} was successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
                catch(BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            if ((sender as Button)!.Content.ToString() == "Update")
            {
                try
                {
                     s_bl.Engineer.Update(CurrentEngineer!);
                    MessageBox.Show($"Engineer {CurrentEngineer!.Id} was successfully Update!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
