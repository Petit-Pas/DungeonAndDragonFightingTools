using DnDToolsLibrary.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfCustomControlLibrary.InputBoxes.IntTextBoxes;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.InputBoxes.DiceTextBoxes
{
    public class BaseD20TextBoxControl : RangedIntTextBoxControl, IRollableControl
    {
        public BaseD20TextBoxControl() : base()
        {
            Min = 0;
            Max = 20;
        }

        public override bool IsValid()
        {
            if (base.IsValid() && IsFullyRolled())
                return true;
            return false;
        }

        public virtual bool IsFullyRolled()
        {
            return !(Integer == 0);
        }

        public virtual void RollControl()
        {
            if (!IsFullyRolled())
            {
                int rolled = DiceRoll.Roll("1d20");
                this.SetValue(IntegerProperty, rolled);
            }
        }
    }
}
