using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            AttackControl.Roll();
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
    }
}
