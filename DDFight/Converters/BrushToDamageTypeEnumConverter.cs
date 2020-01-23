using DDFight.Game.DamageAffinity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DDFight.Converters
{
    public class BrushToDamageTypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case DamageTypeEnum.Acid:
                    return new SolidColorBrush(Colors.YellowGreen);
                case DamageTypeEnum.Bludgeoning:
                    break;
                case DamageTypeEnum.Cold:
                    return new SolidColorBrush(Colors.Blue);
                case DamageTypeEnum.Fire:
                    return new SolidColorBrush(Colors.Red);
                case DamageTypeEnum.Force:
                    break;
                case DamageTypeEnum.Lightning:
                    return new SolidColorBrush(Colors.Yellow);
                case DamageTypeEnum.Necrotic:
                    return new SolidColorBrush(Colors.Black);
                case DamageTypeEnum.Piercing:
                    break;
                case DamageTypeEnum.Poison:
                    return new SolidColorBrush(Colors.Green);
                case DamageTypeEnum.Psychic:
                    return new SolidColorBrush(Colors.Purple);
                case DamageTypeEnum.Radiant:
                    return new SolidColorBrush(Colors.Orange);
                case DamageTypeEnum.Slashing:
                    break;
                case DamageTypeEnum.Thunder:
                    break;
                default:
                    break;
            }
            return Application.Current.Resources["Light"];
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
