using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfCustomControlLibrary.InputBoxes.BaseTextBoxes;
using WpfToolsLibrary.ValidationRules.Numeric;

namespace WpfCustomControlLibrary.InputBoxes.IntTextBoxes
{
    public class PositiveIntTextBoxControl : BaseIntTextBoxControl
    {
        public PositiveIntTextBoxControl() : base ()
        {
        }

        public override ValidationRule GetValidationRule()
        {
            return new PositiveIntValidationRule();
        }
    }
}
