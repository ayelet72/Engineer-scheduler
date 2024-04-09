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
    /// Interaction logic for Gantt.xaml
    /// </summary>
    public partial class GantWindow : Window
    {
        public IEnumerable<BO.Task> GanttList
        {
            get { return (IEnumerable<BO.Task>)GetValue(GanttlistProperty); }
            set { SetValue(GanttlistProperty, value); }
        }
        DependencyProperty GanttlistProperty = DependencyProperty.Register("GantListProperty", typeof(IEnumerable<BO.Task>), typeof(GantWindow), new FrameworkPropertyMetadata(null));

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public GantWindow()
        {
            GanttList = s_bl.Task.ReadAll();
            InitializeComponent();
        }
    }
}