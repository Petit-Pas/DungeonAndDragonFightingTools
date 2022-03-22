using DnDToolsLibrary.Dice;
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
            this.MinWidth = 40;
        }

        protected override void IntegerProperty_Updated(DependencyPropertyChangedEventArgs a)
        {
            base.IntegerProperty_Updated(a);
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
