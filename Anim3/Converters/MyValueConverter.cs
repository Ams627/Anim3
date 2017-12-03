using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace Anim3
{
    public class MultiplyByMinusOne : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return -1 * d;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MyValueConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Duration duration)
            {
                System.Diagnostics.Debug.WriteLine($"duration : {duration.ToString()}");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
