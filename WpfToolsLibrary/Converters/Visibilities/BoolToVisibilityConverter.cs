using BaseToolsLibrary.IO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfToolsLibrary.Converters.Visibilities
{
    /// <summary>
    ///     if the received bool is false, this shall return COLLAPSED, VISIBLE otherwise
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Log("WARNING Converter BoolToVisibilityConverter should not be called this way");
            return default;
        }
    }
}
