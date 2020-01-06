﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace DDFight.ValidationRules
{
    public class PositiveIntRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool valid = false;
            int checked_value = 0;
            Console.WriteLine("COCHON checking validation Rule for uint: " + (string)value);
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
