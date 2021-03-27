using DDFight.Game.Dices;
using DDFight.Tools.UXShortcuts;
using System;
using System.Windows;
using WpfCustomControlLibrary.InputBoxes.IntTextBoxes;

namespace DDFight.Controlers.InputBoxes
{
    public class D20Box : RangedIntTextBoxControl, IRollableControl
    {

        public D20Box() : base()
        {
            Min = 0;
            Max = 20;
        }

        /// <summary>
        ///     Updating the Crit Property
        /// </summary>
        protected override void IntegerProperty_Updated(DependencyPropertyChangedEventArgs a)
        {
            if (Crits == false && Integer == 20)
                Crits = true;
            else if (Crits == true && Integer != 20)
                Crits = false;
        }

        public bool IsFullyRolled()
        {
            try
            {
                return ((int)this.GetValue(IntegerProperty) == 0 ? false : true);
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool Crits
        {
            get { return (bool)this.GetValue(CritProperty); }
            set { this.SetValue(CritProperty, value); }
        }
        public static readonly DependencyProperty CritProperty = DependencyProperty.Register(
          nameof(Crits), typeof(bool), typeof(D20Box), 
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool HasAdvantage
        {
            get { return (bool)this.GetValue(AdvantageProperty); }
            set { this.SetValue(AdvantageProperty, value); }
        }
        public static readonly DependencyProperty AdvantageProperty = DependencyProperty.Register(
          "HasAdvantage", typeof(bool), typeof(D20Box), new PropertyMetadata(false));

        public bool HasDisAdvantage
        {
            get { return (bool)this.GetValue(DisAdvantageProperty); }
            set { this.SetValue(DisAdvantageProperty, value); }
        }
        public static readonly DependencyProperty DisAdvantageProperty = DependencyProperty.Register(
          "HasDisAdvantage", typeof(bool), typeof(D20Box), new PropertyMetadata(false));

        public void RollControl()
        {
            if (!IsFullyRolled())
            {
                int rolled = DiceRoll.Roll("1d20", HasAdvantage, HasDisAdvantage);
                this.SetValue(IntegerProperty, rolled);
            }
        }

        public override bool IsValid()
        {
            if (base.IsValid() && IsFullyRolled())
                return true;
            return false;
        }
    }
}
