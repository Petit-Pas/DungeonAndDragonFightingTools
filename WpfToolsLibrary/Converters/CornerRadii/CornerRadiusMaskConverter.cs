using BaseToolsLibrary.IO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfToolsLibrary.Converters.CornerRadii
{
    public class CornerRadiusMaskConverter : IValueConverter
    {
        /// <summary>
        ///     Expects to recieve a CornerRadius as model, and a string as x,x,x,x
        ///     Xs can either remain x, and will be replaced by the value of the recived model, or they can be integers, and will be added as such
        ///     
        ///     Example, recieving a 5,5,5,5 Corner Radius and sendind 0,0,x,x as parameter will result in a new 0,0,5,5 CornerRadius
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CornerRadius model = (CornerRadius)value;

            try
            {
                string[] masks = ((string)parameter).ToLower().Split(',');

                if (!masks[0].Equals("x"))
                    model.TopLeft = Int32.Parse(masks[0]);
                if (!masks[1].Equals("x"))
                    model.TopRight = Int32.Parse(masks[1]);
                if (!masks[2].Equals("x"))
                    model.BottomRight = Int32.Parse(masks[2]);
                if (!masks[3].Equals("x"))
                    model.BottomLeft = Int32.Parse(masks[3]);
            }
            catch (Exception e)
            {
                Logger.Log($"Exception occured in Convert on CornerRadiusMaskConverter: {e.GetType().ToString()}");
                Logger.Log($"{e.Message}");
            }
            return model;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Log("WARN: calling ConvertBack on CornerRadiusMaskConverter");
            return null;
        }
    }
}
