﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace ERPSystem.Converters
{
    public class NegatableBooleanToVisibilityConverter : IValueConverter
    {
        public NegatableBooleanToVisibilityConverter()
        {
            FalseVisibility = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bVal;
            bool result = bool.TryParse(value.ToString(), out bVal);
            if (!result) return value;
            if (bVal && !Negate) return Visibility.Visible;
            if (bVal && Negate) return FalseVisibility;
            if (!bVal && Negate) return Visibility.Visible;
            if (!bVal && !Negate) return FalseVisibility;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool Negate { get; set; }
        public Visibility FalseVisibility { get; set; }
    }
}
