using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool IsValid()
        {
            return Validation.GetHasError(this);
        }
    }
}
