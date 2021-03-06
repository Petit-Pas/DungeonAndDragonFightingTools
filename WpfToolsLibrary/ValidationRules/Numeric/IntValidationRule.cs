﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfToolsLibrary.ValidationRules.Numeric
{
    public class IntValidationRule : ValidationRule
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

            if (valid == true)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, $"{value}: Should be an integer");
        }
    }
}
