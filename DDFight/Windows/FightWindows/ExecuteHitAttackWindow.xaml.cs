﻿using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Tools;
using DDFight.Tools.UXShortcuts;
using System.Windows;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for ExecuteHitAttackWindow.xaml
    /// </summary>
    public partial class ExecuteHitAttackWindow : Window
    {
        public HitAttackTemplate data_context
        {
            get => (HitAttackTemplate)DataContext;
        }

        public HitAttackResult attackResult;

        public ExecuteHitAttackWindow()
        {
            InitializeComponent();
            Loaded += ExecuteHitAttackWindow_Loaded;
        }

        private void unsubscribe()
        {
            attackResult.PropertyChanged -= AttackResult_PropertyChanged;
            foreach (DamageTemplate dmg in attackResult.DamageList)
            {
                dmg.Damage.PropertyChanged -= AttackResult_PropertyChanged;
            }
        }

        private void subscribe()
        {
            attackResult.PropertyChanged += AttackResult_PropertyChanged;
            foreach (DamageTemplate dmg in attackResult.DamageList)
            {
                dmg.Damage.PropertyChanged += AttackResult_PropertyChanged;
            }
        }

        private void ExecuteHitAttackWindow_Loaded(object sender, RoutedEventArgs e)
        {
            attackResult = data_context.GetResultTemplate();
            AttackControl.DataContext = attackResult;
            subscribe();
            refresh_buttons();
        }

        private void AttackResult_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_buttons();
        }

        void refresh_buttons()
        {
            RollButtonControl.IsEnabled = false;
            ValidateAndExitButtonControl.IsEnabled = false;
            ValidateAndResetButtonControl.IsEnabled = false;
            if (attackResult.HitRoll == 0)
                RollButtonControl.IsEnabled = true;
            foreach (DamageTemplate dmg in attackResult.DamageList)
            {
                if (dmg.Damage.LastRoll == 0)
                    RollButtonControl.IsEnabled = true;
            }
            if (RollButtonControl.IsEnabled == false && attackResult.Target != null && AttackControl.AreAllChildrenValid())
            {
                ValidateAndExitButtonControl.IsEnabled = true;
                ValidateAndResetButtonControl.IsEnabled = true;
            }
        }

        private void RollButtonControl_Click(object sender, RoutedEventArgs e)
        {
            RollableWindowTool.RollRollableChildren(this);
            refresh_buttons();
        }

        private void ValidateAndExitButtonControl_Click(object sender, RoutedEventArgs e)
        {
            AttackControl.Validate();
            this.Close();
        }

        private void ValidateAndResetButtonControl_Click(object sender, RoutedEventArgs e)
        {
            AttackControl.Validate();
            unsubscribe();
            attackResult = data_context.GetResultTemplate();
            AttackControl.DataContext = attackResult;
            subscribe();
            refresh_buttons();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                RollButtonControl_Click(sender, null);
                e.Handled = true;
            }
        }
    }
}
