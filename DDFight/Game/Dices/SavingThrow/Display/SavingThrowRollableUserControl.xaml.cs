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
    /// <summary>
    /// Logique d'interaction pour SavingThrowRollableUserControl.xaml
    /// </summary>
    public partial class SavingThrowRollableUserControl : UserControl, IValidable
    {
        private SavingThrow data_context 
        {
            get => (SavingThrow)DataContext;
        }

        public SavingThrowRollableUserControl()
        {
            InitializeComponent();
            DataContextChanged += SavingThrowRollableUserControl_DataContextChanged;
        }

        private void SavingThrowRollableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            data_context.PropertyChanged += Data_context_PropertyChanged;
        }

        private void Data_context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (data_context.SavingRoll == 0)
            {
                RollButtonControl.IsEnabled = true;
            }
            else
            {
                RollButtonControl.IsEnabled = false;
            }
        }

        public void Roll()
        {
            data_context.SavingRoll = DiceRoll.Roll("1d20", data_context.AdvantageModifiers.SituationalAdvantage, data_context.AdvantageModifiers.SituationalDisadvantage);
        }

        private void RollButtonControl_Click(object sender, RoutedEventArgs e)
        {
            Roll();
        }

        public bool IsValid()
        {
            if (data_context.SavingRoll != 0)
                return true;
            return false;
        }
    }
}
