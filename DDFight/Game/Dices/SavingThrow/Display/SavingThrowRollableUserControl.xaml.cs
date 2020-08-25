using DDFight.Game.Characteristics;
using DDFight.Tools.UXShortcuts;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Game.Dices.SavingThrow.Display
{
    public partial class SavingThrowRollableUserControl : UserControl, IValidable, IRollableControl
    {
        private SavingThrow data_context 
        {
            get {
                try
                {
                    return (SavingThrow)DataContext;
                }
                catch (Exception)
                {
                    return new SavingThrow {
                        Characteristic = CharacteristicsEnum.Dexterity,
                        Difficulty = 9999,
                    };
                }
            }
        }

        public SavingThrowRollableUserControl()
        {
            InitializeComponent();
            DataContextChanged += SavingThrowRollableUserControl_DataContextChanged;
            Loaded += SavingThrowRollableUserControl_Loaded;
        }

        private void refreshSavingModifier()
        {
            int modifier = data_context.Target.Characteristics.GetSavingModifier(data_context.Characteristic);
            CharacteristicTextBlockControl.Text = (data_context.Characteristic + " " + (modifier > 0 ? "+":"" )+ modifier).ToString();
        }

        private void SavingThrowRollableUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            refreshSavingModifier();
        }

        private void SavingThrowRollableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            data_context.PropertyChanged += Data_context_PropertyChanged;
            refreshSavingModifier();
        }

        private void Data_context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // updates roll button
            if (data_context.SavingRoll == 0)
            {
                RollButtonControl.IsEnabled = true;
            }
            else
            {
                RollButtonControl.IsEnabled = false;
            }

            // updates the result of the Saving
            int save_bonus = data_context.Target.Characteristics.GetSavingModifier(data_context.Characteristic);
            int score = data_context.SavingRoll + save_bonus + data_context.Modifier;
            ResultTextBoxControl.Text = score.ToString() + "/" + data_context.Difficulty;
        }

        public void RollControl()
        {
            if (data_context.SavingRoll == 0)
                data_context.SavingRoll = DiceRoll.Roll("1d20", data_context.AdvantageModifiers.SituationalAdvantage, data_context.AdvantageModifiers.SituationalDisadvantage);
        }

        private void RollButtonControl_Click(object sender, RoutedEventArgs e)
        {
            RollControl();
        }

        public bool IsValid()
        {
            if (data_context.SavingRoll != 0)
                return true;
            return false;
        }
    }
}
