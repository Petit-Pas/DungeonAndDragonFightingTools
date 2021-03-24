using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfCustomControlLibrary.InputBoxes.BaseTextBoxes
{
    public abstract class ValidableTextBoxControl : BaseTextBoxControl
    {
        public ValidableTextBoxControl() : base()
        {
        }

        public abstract ValidationRule GetValidationRule();
    }
}
