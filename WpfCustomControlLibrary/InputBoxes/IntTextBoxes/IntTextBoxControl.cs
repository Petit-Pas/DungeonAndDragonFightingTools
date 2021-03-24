using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules.Numeric;

namespace WpfCustomControlLibrary.InputBoxes.IntTextBoxes
{
    public class IntTextBoxControl : BaseIntTextBoxControl
    {
        private readonly ValidationRule _validationRule = new IntValidationRule();

        public IntTextBoxControl() : base()
        {
        }

        public override ValidationRule GetValidationRule()
        {
            return _validationRule;
        }
    }
}
