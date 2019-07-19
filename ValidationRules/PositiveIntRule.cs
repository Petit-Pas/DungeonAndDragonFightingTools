// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DDFight.ValidationRules
{
    public class PositiveIntRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool valid = false;
            int checked_value = 0;
            try
            {
                checked_value = Int32.Parse((string)value);
                valid = true;
            }
            catch (Exception)
            {
            }

            if (checked_value >= 0 && valid == true)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Should be a positive integer");
        }
    }
}
