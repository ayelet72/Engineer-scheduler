using PL.Engineer;
using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for EnteringIDWindow.xaml
    /// </summary>
    public partial class EnteringIDWindow : Window
    {
       
        public static readonly DependencyProperty EngineerIDProperty =
        DependencyProperty.Register("EngineerID", typeof(string), typeof(EnteringIDWindow), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string EngineerID
        {
            get { return (string)GetValue(EngineerIDProperty); }
            set { SetValue(EngineerIDProperty, value); }
        }
        
        public EnteringIDWindow()
        {
            InitializeComponent();
            DataContext = this;
            
           
        }

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();



        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(EngineerID, out int id);

            // Check if the entered engineer ID exists in the list of engineers
            if (s_bl.Engineer.ReadAll().Any(engineer => engineer.Id == id))
            {
                // Engineer ID is valid - open the EngineerViewWindow
                EngineerViewWindow engineerViewWindow = new EngineerViewWindow(id);
                BO.Engineer engineer = s_bl.Engineer.Read(id)!;
                engineerViewWindow.DataContext = engineer;
                engineerViewWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Engineer ID not found. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Update the EngineerID when the user types in the PasswordBox
            EngineerID += e.Text;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineerID = (sender as PasswordBox).Password;
        }


    }
}
