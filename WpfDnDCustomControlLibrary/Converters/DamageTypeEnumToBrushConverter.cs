using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Attacks.Damage.Type;
using System;
using System.Globalization;
using System.Windows.Data;
using WpfToolsLibrary.ConsoleTools;

namespace WpfDnDCustomControlLibrary.Converters
{
    public class DamageTypeEnumToBrushConverter : IValueConverter
    {
        private WpfFontColorProvider colorProvider = DIContainer.GetImplementation<IFontColorProvider>() as WpfFontColorProvider;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DamageTypeEnum val = (DamageTypeEnum)value;
            WpfFontColor color = colorProvider.GetColorByKey(val.ToString()) as WpfFontColor;
            return color.Brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // --> to enum
            return default;
        }
    }
}
