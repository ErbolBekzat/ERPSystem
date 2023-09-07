using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using ERPSystem.Views.WorkerView;

namespace ERPSystem.Converters
{
    public class AttendanceConverter : IMultiValueConverter
    {
        public bool Negate { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is WorkerListViewModel viewModel && values[1] is int workerId)
            {
                string attendanceStatus = viewModel.LoadAttendanceInfo(workerId);

                if (attendanceStatus == "not exists")
                {
                    if (Negate)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else if (attendanceStatus == "check out time")
                {
                    if (Negate)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
