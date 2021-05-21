using BaseToolsLibrary.IO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfToolsLibrary.Converters.Visibilities
{
    public class IntGreaterThanZeroToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Log("WARNING Converter IntGreaterThanZeroToVisibility should not be called this way");
            return default;
        }
    }
}
