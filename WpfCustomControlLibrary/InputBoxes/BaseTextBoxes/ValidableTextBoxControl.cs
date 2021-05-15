using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules;

namespace WpfCustomControlLibrary.InputBoxes.BaseTextBoxes
{
    public abstract class ValidableTextBoxControl : BaseTextBoxControl, IValidable
    {
        public ValidableTextBoxControl() : base()
        {
        }

        public abstract ValidationRule GetValidationRule();

        public virtual bool IsValid()
        {
            return !Validation.GetHasError(this);
        }
    }
}
