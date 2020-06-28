using DDFight.Game.Dices;
using DDFight.Game.Status;
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

namespace DDFight.Game.Aggression.Attacks.Display
{
    /// <summary>
    /// Logique d'interaction pour HitAttackResultEditableControl.xaml
    /// </summary>
    public partial class HitAttackResultEditableControl : UserControl
    {
        private HitAttackResult data_context
        {
            get => (HitAttackResult)DataContext;
        }

        public HitAttackResultEditableControl()
        {
            InitializeComponent();
            Loaded += HitAttackResultEditableControl_Loaded;
            DataContextChanged += HitAttackResultEditableControl_DataContextChanged;
        }

        private void HitAttackResultEditableControl_LayoutUpdated(object sender, EventArgs e)
        {
            //TODO this should not be, it juste spams the function way too much, the problem is to find a way to refresh buttons when one of the damageTemplate.Damage.LastRoll gets updated
            refresh_buttons();
        }

        private void HitAttackResultEditableControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                data_context.PropertyChanged += Data_context_PropertyChanged;
                data_context.SituationalHitAttackModifiers.PropertyChanged += SituationalHitAttackModifiers_PropertyChanged;
                LayoutUpdated += HitAttackResultEditableControl_LayoutUpdated;
            }
            catch (Exception)
            { }
        }

        private void HitAttackResultEditableControl_Loaded(object sender, RoutedEventArgs e)
        {
            HitAttackTargetComboControl.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
            HitAttackTargetComboControl.SelectionChanged += HitAttackTargetComboControl_SelectionChanged;
        }

        private void DamageControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_buttons();
        }

        private void HitAttackTargetComboControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HitAttackTargetComboControl.SelectedIndex == -1)
                data_context.Target = null;
            else
                data_context.Target = (PlayableEntity)HitAttackTargetComboControl.SelectedItem;
            refresh_buttons();
        }

        private void SituationalHitAttackModifiers_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_hit();
            refresh_hit_target();
            refresh_buttons();
        }

        void refresh_buttons()
        {
            RollButtonControl.IsEnabled = false;
            ValidateButtonControl.IsEnabled = false;
            if (data_context.HitRoll == 0)
                RollButtonControl.IsEnabled = true;
            foreach (DamageTemplate dmg in data_context.DamageList)
            {
                if (dmg.Damage.LastRoll == 0)
                    RollButtonControl.IsEnabled = true;
            }
            if (RollButtonControl.IsEnabled == false && data_context.Target != null)
                ValidateButtonControl.IsEnabled = true;
        }

        void refresh_hit_target()
        {
            HitTargetControl.Text = "0";
            if (data_context.Target != null)
            {
                HitTargetControl.Text = (data_context.Target.CA + data_context.SituationalHitAttackModifiers.ACModifier).ToString();
            }
        }

        void refresh_hit()
        {
            HitResultControl.Text = (data_context.HitBonus + data_context.HitRoll + data_context.SituationalHitAttackModifiers.HitModifier).ToString();
        }

        private void Data_context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_hit();
            refresh_hit_target();
            refresh_buttons();
        }

        private void RollButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (data_context.HitRoll == 0)
                data_context.HitRoll = (uint)DiceRoll.Roll("1d20", data_context.SituationalAdvantageModifiers.SituationalAdvantage, data_context.SituationalAdvantageModifiers.SituationalDisadvantage);
            foreach (DamageTemplate dmg in data_context.DamageList)
            {
                if (dmg.Damage.LastRoll == 0)
                {
                    dmg.Damage.Roll(data_context.HitRoll >= 20 ? true : false);
                }
            }
            refresh_buttons();
        }

        private void ValidateButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                bool hit = data_context.Target.GetAttacked(data_context, data_context.Owner);
                if (hit == true && data_context.OnHitStatuses.List.Count != 0)
                {
                    foreach (OnHitStatus onHitStatus in data_context.OnHitStatuses.List)
                    {
                        onHitStatus.Apply(data_context.Owner, data_context.Target);
                    }
                }
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
