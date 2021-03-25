﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules.Text;

namespace WpfCustomControlLibrary.InputBoxes.StringTextBoxes
{
    public class NotEmptyStringTextBoxControl : BaseStringTextBoxControl
    {
        private readonly ValidationRule _validationRule = new NotEmptyStringValidationRule();

        public NotEmptyStringTextBoxControl() : base()
        {
        }

        public override ValidationRule GetValidationRule()
        {
            return _validationRule;
        }
    }
}
