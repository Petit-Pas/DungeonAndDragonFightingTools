using DDFight.Tools.UXShortcuts;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Dices.SavingThrow.Display
{
    /// <summary>
    /// Interaction logic for SavingThrowRollableDenseUserControl.xaml
    /// </summary>
    public partial class SavingThrowRollableDenseUserControl : UserControl, IRollableControl
    {

        private SavingThrow data_context
        {
            get 
            {
                try
                {
                    return (SavingThrow)DataContext;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public SavingThrowRollableDenseUserControl()
        {
            InitializeComponent();

            DataContextChanged += SavingThrowRollableDenseUserControl_DataContextChanged;
        }

        private void SavingThrowRollableDenseUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
            {
                int modifier = data_context.Target.Characteristics.GetSavingModifier(data_context.Characteristic);
                AbilityModifierControl.Text = modifier < 0 ? modifier.ToString() : "+" + modifier.ToString();
                data_context.PropertyChanged += Data_context_PropertyChanged;
            }
        }

        private void Data_context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refreshResult();
        }

        private void refreshResult()
        {
            if (data_context != null)
            {
                int result = data_context.Target.Characteristics.GetSavingModifier(data_context.Characteristic) + data_context.SavingRoll + data_context.Modifier;
                int target = data_context.Difficulty;

                ResultControl.Text = result + "/" + target;
            }
        }
        
        public void RollControl()
        {
            if (data_context != null)
            {
                if (data_context.SavingRoll == 0)
                {
                    data_context.SavingRoll = DiceRoll.Roll("1d20", data_context.AdvantageModifiers.SituationalAdvantage, data_context.AdvantageModifiers.SituationalDisadvantage);
                }
            }
        }

        public bool IsFullyRolled()
        {
            if (data_context == null)
                return true;
            if (data_context.SavingRoll == 0)
            {
                return false;
            }
            return true;
        }
    }
}
