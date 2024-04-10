using BO;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL;

 class TaskToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool showButton = false;
        if (value != null && parameter != null && value is BO.Task task && parameter is string boolParameter)
        {
            bool parameterValue = bool.TryParse(boolParameter, out bool result) ? result : false;
            showButton = (task != null && parameterValue) || (task == null && !parameterValue);
        }

        return showButton ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //if id ==0 -> "Add" else -> "Update"
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
 class IdToEnabledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        if ((int)value == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
 class TaskDetailsConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value == null)

        {
            return "There is no current task assigned to this engineer.";
        }
        else
        {
            // פרטי המשימה
            return s_bl.Task.Read(((BO.Task)value).Id);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
class EffotTimeToWidthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return 0;
        }
        else
        {
            // פרטי המשימה
            return ((TimeSpan)value).TotalDays * 5;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class StartDateToMarginConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return 0;
        }
        else
        {
            // פרטי המשימה
            return new Thickness((((DateTime)value).Subtract(s_bl.StartProject ?? DateTime.Now).TotalDays) * 5 + 100, 0, 0, 0);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ColorConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return 0;
        }
        else
        {
            
            if ((BO.Status)value == BO.Status.Scheduled) 
                return Brushes.LightBlue;
            if ((BO.Status)value == BO.Status.OnTrack)
                return Brushes.LightPink;
            if ((BO.Status)value == BO.Status.Done)
                return Brushes.Green;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
