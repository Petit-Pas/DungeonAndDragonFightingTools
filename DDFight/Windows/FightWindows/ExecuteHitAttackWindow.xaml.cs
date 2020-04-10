using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        public ExecuteHitAttackWindow()
        {
            InitializeComponent();
            Loaded += ExecuteHitAttackWindow_Loaded;
        }

        private List<HitAttackResult> attacks = new List<HitAttackResult>();

        private void ExecuteHitAttackWindow_Loaded(object sender, RoutedEventArgs e)
        {
            attacks = data_context.GetResultTemplate();
            AttackList.ItemsSource = attacks;
        }
    }
}
