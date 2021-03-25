using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfToolsLibrary.ValidationRules.Text
{
    public class NotEmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
                return new ValidationResult(false, "Should be a non empty string");
            return ValidationResult.ValidResult;
        }
    }
}
