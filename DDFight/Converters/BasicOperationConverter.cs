using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DDFight.Converters
{

    public class BasicOperationConverter : IMultiValueConverter
    {

        private int _convert(List<int> operands, string expression, int default_value)
        {
            int amount_operands = expression.Count((c) => c == 'x');

            if (amount_operands != operands.Count)
            {
                Logger.Log(String.Format("WARN: amount of operands didnt match in operation {0} in BasicOperationConverter", expression));
                return default_value;
            }

            foreach (int operand in operands)
            {
                expression = expression.ReplaceFirst("x", operand.ToString());
            }

            int result = ArithmeticParser.EvaluateExpression(expression);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">
        ///     expects 2 parameters maximum :
        ///         - mandatory: an arithmetic expression, in which 'x's will be replaced by bounded variables in the recieved order
        ///         - optional: a default value (1 by default) in case an exception occured in the evaluation of the arithmetic expression, or if the amount of variables didnt match the amount of 'x's
        ///         
        ///         Warning: calculations will be done with ints, but return value is double (as wpf properties are often doubles)
        /// </param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> operands = new List<int>();
            int default_value = 1;

            try
            {
                string[] result = ((string)parameter).Split('|');
                default_value = result.Length > 1 ? Int32.Parse(result[1]) : default_value;
                string expression = result[0];
                foreach (object value in values)
                {
                    operands.Add((int)((double)value));
                }
                return (double)_convert(operands, expression, default_value);
            }
            catch (Exception e) 
            {
                Logger.Log("WARN: exception in BasicOperationConverter.Convert(): " + e.Message);
                return default_value; 
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
