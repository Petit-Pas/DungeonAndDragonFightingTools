using DnDToolsLibrary.Characteristics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfDnDCustomControlLibrary.Converters
{
    public class CharacteristicToShortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CharacteristicsEnum charac = (CharacteristicsEnum)value;
            return charac.ToString().Substring(0, 3).ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
