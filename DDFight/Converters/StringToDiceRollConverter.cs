using DDFight.Game.Dices;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DDFight.Converters
{
    public class StringToDiceRollConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new DiceRoll((string)value);
        }
    }
}
