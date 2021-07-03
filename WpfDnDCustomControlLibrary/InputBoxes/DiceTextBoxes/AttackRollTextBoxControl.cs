using System;
using System.Windows;

namespace WpfDnDCustomControlLibrary.InputBoxes.DiceTextBoxes
{
    public class AttackRollTextBoxControl : SituationalD20TextBoxControl
    {
        public AttackRollTextBoxControl() : base()
        {
        }

        protected override void IntegerProperty_Updated(DependencyPropertyChangedEventArgs a)
        {
            Console.WriteLine("BHDEBUG: in intergerPropertyUpdated");
            base.IntegerProperty_Updated(a);

            if (Crits == false && Integer == 20)
                Crits = true;
            else if (Crits == true && Integer != 20)
                Crits = false;
        }

        public bool Crits
        {
            get { return (bool)this.GetValue(CritProperty); }
            set { this.SetValue(CritProperty, value); }
        }
        public static readonly DependencyProperty CritProperty = DependencyProperty.Register(
          nameof(Crits), typeof(bool), typeof(AttackRollTextBoxControl),
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
