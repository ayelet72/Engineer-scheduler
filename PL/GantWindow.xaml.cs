using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for Gantt.xaml
/// </summary>
public partial class GantWindow : Window
{
    public IEnumerable<BO.Task> GanttList
    {
        get { return (IEnumerable<BO.Task>)GetValue(GanttListProperty); }
        set { SetValue(GanttListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for GanttList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GanttListProperty =
        DependencyProperty.Register("GanttList", typeof(IEnumerable<BO.Task>), typeof(GantWindow), new PropertyMetadata(null));


    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public GantWindow()
    {
        GanttList = s_bl.Task.ReadAll();
        InitializeComponent();
    }
}x