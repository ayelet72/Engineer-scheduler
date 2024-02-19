using System.Globalization;
using System.Windows.Data;

namespace PL;

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
        // כאן אתה יכול לבצע כל סוג של לוגיקה שברצונך, לדוגמה:
        // אם הערך הוא 0, החזר False לשביתת הפקד. אחרת, החזר True לאפשרויות הפקד.
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
