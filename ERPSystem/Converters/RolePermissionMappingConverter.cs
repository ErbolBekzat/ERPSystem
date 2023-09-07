using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ERPSystem.Converters
{
    public class RolePermissionMappingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is string permissionName) || !(values[1] is ObservableCollection<RolePermissionMapping> rolePermissionMappings))
                return false;

            bool recordExists = rolePermissionMappings.Any(mapping => mapping.PermissionName == permissionName);

            return recordExists;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
