// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using System;
using System.Globalization;
using System.Windows.Data;

namespace DDFight.Converters
{
    public class StringIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Int32.Parse((string)value);
        }
    }
}
