using System.Globalization;
using System.Windows.Controls;

namespace DDFight.ValidationRules
{
    public class NotEmptyStringRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            System.Console.WriteLine("received: [{0}]", (string)value);
            string text = (string)value;
            if (text != null && text != string.Empty && text != "")
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Should be a non empty string");
        }
    }
}
