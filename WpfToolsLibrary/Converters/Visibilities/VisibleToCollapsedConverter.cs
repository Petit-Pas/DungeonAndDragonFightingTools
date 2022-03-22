using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfToolsLibrary.Converters.Visibilities
{
    public class VisibleToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility vis)
                return vis == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
