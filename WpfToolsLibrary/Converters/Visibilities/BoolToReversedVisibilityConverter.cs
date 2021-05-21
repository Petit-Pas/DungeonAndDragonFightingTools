using BaseToolsLibrary.IO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfToolsLibrary.Converters.Visibilities
{
    /// <summary>
    ///     if the received bool is true, this shall return COLLAPSED, VISIBLE otherwise
    /// </summary>
    public class BoolToReversedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Log("WARNING Converter BoolToReversedVisibilityConverter should not be called this way");
            return default;
        }
    }
}
