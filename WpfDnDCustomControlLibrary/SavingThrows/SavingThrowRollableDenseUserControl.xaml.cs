using DnDToolsLibrary.Dice;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.SavingThrows
{
    /// <summary>
    /// Interaction logic for SavingThrowRollableDenseUserControl.xaml
    /// </summary>
    public partial class SavingThrowRollableDenseUserControl : UserControl, IRollableControl
    {

        public SavingThrow SavingThrow
        {
            get { return (SavingThrow)this.GetValue(savingThrowProperty); }
            set { this.SetValue(savingThrowProperty, value); }
        }
        private static readonly DependencyProperty savingThrowProperty = DependencyProperty.Register(
            nameof(SavingThrow),
            typeof(SavingThrow),
            typeof(SavingThrowRollableDenseUserControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, a) => ((SavingThrowRollableDenseUserControl)o).SavingThrow_Changed(o, a))
        );

        public SavingThrowRollableDenseUserControl()
        {
            InitializeComponent();
        }

        private void SavingThrow_Changed(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is SavingThrow oldSaving)
                oldSaving.PropertyChanged -= savingPropertyChanged;

            if (SavingThrow != null)
            {
                int modifier = SavingThrow.Target?.Characteristics.GetSavingModifier(SavingThrow.Characteristic) ?? 0;
                AbilityModifierControl.Text = modifier < 0 ? modifier.ToString() : "+" + modifier.ToString();
                AbilityModifierControl2.Text = modifier < 0 ? modifier.ToString() : "+" + modifier.ToString();
                SavingThrow.PropertyChanged += savingPropertyChanged;
            }
        }

        private void savingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refreshResult();
        }

        private void refreshResult()
        {
            if (SavingThrow != null)
            {
                int result = SavingThrow.Target.Characteristics.GetSavingModifier(SavingThrow.Characteristic) + SavingThrow.SavingRoll + SavingThrow.Modifier;
                int target = SavingThrow.Difficulty;

                ResultControl.Text = result + "/" + target;
            }
        }
        
        public void RollControl()
        {
            if (SavingThrow != null)
            {
                if (SavingThrow.SavingRoll == 0)
                {
                    SavingThrow.SavingRoll = DiceRoll.Roll("1d20", SavingThrow.AdvantageModifiers.SituationalAdvantage, SavingThrow.AdvantageModifiers.SituationalDisadvantage);
                }
            }
        }

        public bool IsFullyRolled()
        {
            if (SavingThrow == null)
                return true;
            if (SavingThrow.SavingRoll == 0)
            {
                return false;
            }
            return true;
        }
    }
}
