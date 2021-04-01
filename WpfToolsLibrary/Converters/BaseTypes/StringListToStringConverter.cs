using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace WpfToolsLibrary.Converters.BaseTypes
{
    public class StringListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // from List<string> to string
            List<string> list = (List<string>)value;
            string result = "";

            foreach (string str in list)
            {
                result += str + "\r\n";
            }
            return result;
        }

        // TODO should use split() instead 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // from string to List<string>

            string full = (string)value;
            List<string> result = new List<string>();

            int start = 0;

            while (true)
            {
                string tmp = "";
                int next = full.IndexOf("\r\n", start);
                if (next == -1)
                {
                    tmp = full.Substring(start);
                    result.Add(tmp);
                    break;
                }
                tmp = full.Substring(start, next - start);
                start = next + 2;
                result.Add(tmp);
            }
            return result;
        }
    }
}
