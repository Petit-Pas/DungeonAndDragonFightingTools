﻿using DnDToolsLibrary.Dice;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.Navigation;
using WpfToolsLibrary.ValidationRules;

namespace WpfDnDCustomControlLibrary.SavingThrows
{
    public partial class SavingThrowRollableUserControl : UserControl, IValidable, IRollableControl
    {
        private SavingThrow data_context 
        {
            get => DataContext as SavingThrow;
        }

        public SavingThrowRollableUserControl()
        {
            DataContextChanged += SavingThrowRollableUserControl_DataContextChanged;
            InitializeComponent();
        }

        private void refreshSavingModifier()
        {
            if (data_context != null)
            {
                int modifier = data_context.Target.Characteristics.GetSavingModifier(data_context.Characteristic);
                ModifierTextBoxControl.Text = $"{(modifier >= 0 ? "+" : "")}{modifier}";
            }
        }

        private void SavingThrowRollableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is SavingThrow oldDataContext)
                oldDataContext.PropertyChanged -= Data_context_PropertyChanged;
            if (data_context != null)
            {
                data_context.PropertyChanged += Data_context_PropertyChanged;
                refreshSavingModifier();
            }
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
            ResultTextBoxControl.Text = $"{score}/{data_context.Difficulty}";
        }

        public void RollControl()
        {
            if (data_context != null)
            {
                if (data_context.SavingRoll == 0)
                    data_context.SavingRoll = DiceRoll.Roll("1d20", data_context.AdvantageModifiers.SituationalAdvantage, data_context.AdvantageModifiers.SituationalDisadvantage);
            }
        }

        public bool IsFullyRolled()
        {
            return data_context.SavingRoll != 0;
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

        public string GetValidationErrorMessage()
        {
            throw new NotImplementedException();
        }
    }
}
