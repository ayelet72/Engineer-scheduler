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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    /// 

    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.None;
        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Level == BO.EngineerExperience.None) ?
                s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == (DO.EngineerExperience)Level)!;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EngineerWindow engineerWindow = new EngineerWindow();
            engineerWindow.Closed += EngineerWindow_Closed!;
            new EngineerWindow().ShowDialog();
        }

        private void EngineerList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
            if (engineer != null)
            {
                EngineerWindow engineerWindow = new EngineerWindow(engineer.Id);
                engineerWindow.Closed += EngineerWindow_Closed!;
                engineerWindow.ShowDialog();

            }
        }
        private void EngineerWindow_Closed(object sender, EventArgs e)
        {
            // קריאה לפונקציה לעדכון רשימת הפריטים ב- ListView
            UpdateEngineerList();
        }
        private void UpdateEngineerList()
        {
            List<BO.Engineer> engineers = s_bl.Engineer.ReadAll().ToList(); // דוגמה לקריאה לבסיס הנתונים או לשירות

            // עדכון של מקור הנתונים של ה- ListView עם רשימת המהנדסים העדכנית
            EngineerList = engineers;
        }
       
    }

}
