using DDFight.Game.Entities;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DDFight.Converters
{
    public class VisibleIfCharacter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(Character))
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
