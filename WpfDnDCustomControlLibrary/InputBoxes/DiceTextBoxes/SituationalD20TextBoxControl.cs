using DnDToolsLibrary.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDnDCustomControlLibrary.InputBoxes.DiceTextBoxes
{
    /// <summary>
    ///     Adds Situational Advantage and Disadvantage properties to the D20
    /// </summary>
    public class SituationalD20TextBoxControl : BaseD20TextBoxControl
    {
        public SituationalD20TextBoxControl() : base()
        {
        }

        public override void RollControl()
        {
            if (!IsFullyRolled())
            {
                int rolled = DiceRoll.Roll("1d20", HasAdvantage, HasDisAdvantage);
                this.SetValue(IntegerProperty, rolled);
            }
        }

        public bool HasAdvantage
        {
            get { return (bool)this.GetValue(AdvantageProperty); }
            set { this.SetValue(AdvantageProperty, value); }
        }
        public static readonly DependencyProperty AdvantageProperty = DependencyProperty.Register(
          "HasAdvantage", typeof(bool), typeof(SituationalD20TextBoxControl), new PropertyMetadata(false));

        public bool HasDisAdvantage
        {
            get { return (bool)this.GetValue(DisAdvantageProperty); }
            set { this.SetValue(DisAdvantageProperty, value); }
        }
        public static readonly DependencyProperty DisAdvantageProperty = DependencyProperty.Register(
          "HasDisAdvantage", typeof(bool), typeof(SituationalD20TextBoxControl), new PropertyMetadata(false));
    }
}
