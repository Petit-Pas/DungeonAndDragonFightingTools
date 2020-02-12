using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DDFight.ValidationRules
{
    public class DiceRollRule : ValidationRule
    {
        static Regex rgx = new Regex(@"^((([[1-9]+[0-9]*d[1-9]+[0-9]*]*)(\+[[1-9]+[0-9]*d[1-9]+[0-9]*]*)*(\+[1-9]+[0-9]*)?)|([1-9]+[0-9]*))$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string format = (string)value;

            MatchCollection matched = rgx.Matches(format);
            if (matched.Count == 1)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Dice Format is not right");
        }
    }
}
