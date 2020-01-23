using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DDFight.Converters
{
    public class BrushToDamageTypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // --> to brush
            Console.WriteLine("in convert to brush");
            Console.WriteLine(value.GetType().ToString());
            SolidColorBrush result = new SolidColorBrush(Colors.Red);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // --> to enum
            Console.WriteLine("in convert from brush");
            Console.WriteLine(value.GetType().ToString());
            return default;
        }
    }
}
