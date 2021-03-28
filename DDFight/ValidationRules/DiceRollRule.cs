using DDFight.Game.Dices;
using DnDToolsLibrary.Dice;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DDFight.ValidationRules
{
    public class DiceRollRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string format = (string)value;

            Match match = DiceRoll.rgx.Match(format);
            //MatchCollection matched = rgx.Matches(format);
            if (match.Success)
                return new ValidationResult(true, null);
            return new ValidationResult(false, "Dice Format is not right");
        }
    }
}
