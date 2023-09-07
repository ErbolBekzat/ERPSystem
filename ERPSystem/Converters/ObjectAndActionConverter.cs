using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ERPSystem.Converters
{
    public class ObjectAndActionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                string objName = values[0] as string;
                string action = values[1] as string;
                return $"{objName} {action}";
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return ((string)value)?.Split(',').ToArray();
        }
    }
}
