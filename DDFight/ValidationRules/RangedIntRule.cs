﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace DDFight.ValidationRules
{
    public class RangedIntRule : ValidationRule
    {
        public RangedIntRule()
        {
        }

        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int nb = 0;

            try
            {
                if (((string)value).Length > 0)
                    nb = Int32.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, $"Please enter a number in the range: {Min}-{Max}.");
            }

            if ((nb < Min) || (nb > Max))
            {
                return new ValidationResult(false,
                  $"Please enter a number in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
