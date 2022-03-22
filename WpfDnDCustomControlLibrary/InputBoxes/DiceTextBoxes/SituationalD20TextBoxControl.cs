using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Dice;
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
                int rolled = DiceRoll.Roll("1d20", AdvantageModifiers.SituationalAdvantage, AdvantageModifiers.SituationalDisadvantage);
                Integer = rolled;
            }
        }

        public SituationalAdvantageModifiers AdvantageModifiers
        {
            get { return (SituationalAdvantageModifiers)this.GetValue(AdvantageModifiersProperty); }
            set { this.SetValue(AdvantageModifiersProperty, value); }
        }
        private static readonly DependencyProperty AdvantageModifiersProperty = DependencyProperty.Register(
            nameof(AdvantageModifiers),
            typeof(SituationalAdvantageModifiers),
            typeof(SituationalD20TextBoxControl),
            new PropertyMetadata(new SituationalAdvantageModifiers()));
    }
}
