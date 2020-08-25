﻿using DDFight.Game.Dices;
using DDFight.Game.Status;
using DDFight.Tools.UXShortcuts;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Aggression.Attacks.Display
{
    /// <summary>
    /// Logique d'interaction pour HitAttackResultEditableControl.xaml
    /// </summary>
    public partial class HitAttackResultEditableControl : UserControl, IRollableControl
    {
        private HitAttackResult data_context
        {
            get => (HitAttackResult)DataContext;
        }

        public HitAttackResultEditableControl()
        {
            InitializeComponent();
            DataContextChanged += HitAttackResultEditableControl_DataContextChanged;
            Loaded += HitAttackResultEditableControl_Loaded;
        }

        private void HitAttackResultEditableControl_Loaded(object sender, RoutedEventArgs e)
        {
            HitAttackTargetComboControl.Focus();
        }

        private void HitAttackResultEditableControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            HitAttackTargetComboControl.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
            HitAttackTargetComboControl.SelectionChanged += HitAttackTargetComboControl_SelectionChanged;
            try
            {
                data_context.PropertyChanged += Data_context_PropertyChanged;
                data_context.SituationalHitAttackModifiers.PropertyChanged += SituationalHitAttackModifiers_PropertyChanged;
                if (HitAttackTargetComboControl.SelectedIndex != -1)
                    data_context.Target = (PlayableEntity)HitAttackTargetComboControl.SelectedItem;
                refresh_hit();
                refresh_hit_target();
            }
            catch (Exception)
            {
            }
        }

        private void HitAttackTargetComboControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HitAttackTargetComboControl.SelectedIndex == -1)
                data_context.Target = null;
            else
                data_context.Target = (PlayableEntity)HitAttackTargetComboControl.SelectedItem;
        }

        private void SituationalHitAttackModifiers_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_hit();
            refresh_hit_target();
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
            HitResultControl.Text = "0";
            if (data_context.HitRoll != 0)
                HitResultControl.Text = (data_context.HitBonus + data_context.HitRoll + data_context.SituationalHitAttackModifiers.HitModifier).ToString();
        }

        private void Data_context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_hit();
            refresh_hit_target();
        }

        public void RollControl()
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
        }

        public void Validate()
        {
            if (this.AreAllChildrenValid())
            {
                bool hit = data_context.Target.GetAttacked(data_context, data_context.Owner);
                if (hit == true && data_context.OnHitStatuses.List.Count != 0)
                {
                    foreach (OnHitStatus onHitStatus in data_context.OnHitStatuses.List)
                    {
                        onHitStatus.CheckIfApply(data_context.Owner, data_context.Target);
                    }
                }
            }
        }
    }
}