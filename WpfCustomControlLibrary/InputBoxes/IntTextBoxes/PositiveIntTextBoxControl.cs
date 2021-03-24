using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules.Numeric;

namespace WpfCustomControlLibrary.InputBoxes.IntTextBoxes
{
    public class PositiveIntTextBoxControl : BaseIntTextBoxControl
    {

        private ValidationRule _validationRule = new PositiveIntValidationRule();

        public PositiveIntTextBoxControl() : base ()
        {
        }

        public override ValidationRule GetValidationRule()
        {
            return _validationRule;
        }
    }
}
