using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Game.Aggression.Attacks.Display
{
    /// <summary>
    /// Logique d'interaction pour HitAttackResultEditableControl.xaml
    /// </summary>
    public partial class HitAttackResultEditableControl : UserControl, IRollableControl
    {
        private HitAttackResult data_context
        {
            get => DataContext as HitAttackResult;
        }

        public HitAttackResultEditableControl()
        {
            DataContextChanged += HitAttackResultEditableControl_DataContextChanged;
            Loaded += HitAttackResultEditableControl_Loaded;
            Initialized += HitAttackResultEditableControl_Initialized;
            InitializeComponent();
        }

        private void HitAttackResultEditableControl_Initialized(object sender, EventArgs e)
        {
            HitAttackTargetComboControl.ItemsSource = Global.Context.FightContext.FightersList.Elements;
            HitAttackTargetComboControl.SelectionChanged += HitAttackTargetComboControl_SelectionChanged;
        }

        private void HitAttackResultEditableControl_Loaded(object sender, RoutedEventArgs e)
        {
            HitAttackTargetComboControl.Focus();
        }

        private void HitAttackResultEditableControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
            {
                if (HitAttackTargetComboControl.SelectedIndex != -1)
                    data_context.Target = (PlayableEntity)HitAttackTargetComboControl.SelectedItem;
                else
                    data_context.Target = null;
            }
        }

        private void HitAttackTargetComboControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data_context != null)
            {
                if (HitAttackTargetComboControl.SelectedIndex != -1)
                    data_context.Target = (PlayableEntity)HitAttackTargetComboControl.SelectedItem;
                else
                    data_context.Target = null;
            }
        }

        public void RollControl()
        {
            if (data_context.RollResult.AttackRoll == 0)
                data_context.RollResult.AttackRoll = DiceRoll.Roll("1d20", data_context.RollResult.AdvantageModifiers.SituationalAdvantage, data_context.RollResult.AdvantageModifiers.SituationalDisadvantage);
            foreach (DamageResult dmg in data_context.DamageList.Elements)
            {
                if (dmg.Damage.LastRoll == 0)
                {
                    dmg.Damage.Roll(data_context.RollResult.Crits);
                }
            }
        }

        public bool IsFullyRolled()
        {
            if (data_context.RollResult.AttackRoll == 0)
                return false;
            foreach (DamageResult dmg in data_context.DamageList.Elements)
            {
                if (dmg.Damage.LastRoll == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void Validate()
        {
            if (this.AreAllChildrenValid())
                data_context.RollResult.Target.GetAttacked(data_context, data_context.Owner);
        }
    }
}
