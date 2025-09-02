using Humanizer;
using System.Globalization;
using System.Windows.Data;

namespace TodoList.MVVM.ToolKit.Converters
{
    public class HumanizerDateTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.Humanize(false);
            }

            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.Humanize();
            }

            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
