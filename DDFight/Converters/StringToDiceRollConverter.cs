using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DDFight.Converters
{
    public class StringToDiceRollConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine("in Convert with received: " + value.GetType().ToString());
            Console.WriteLine("should output: " + value.ToString());
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new DiceRoll((string)value);
        }
    }
}
