﻿using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DDFight.Converters
{
    /// <summary>
    ///     if the received string is empty, this shall return COLLAPSED, VISIBLE otherwise
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (String.IsNullOrEmpty(str))
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Log("WARNING Converter StringToVisibilityConverter should not be called this way");
            return default;
        }
    }
}
