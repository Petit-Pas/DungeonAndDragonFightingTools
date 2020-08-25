using System;
using System.Globalization;
using System.Windows.Data;

namespace DDFight.Converters
{
    public class BoolToCustomStringConverter : IValueConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"> Parameter will be parsed as param1|param2, param1 is returned if value is true </param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] values = ((string)parameter).Split('|');
            if ((bool)value == true)
                return values[0];
            return values[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
