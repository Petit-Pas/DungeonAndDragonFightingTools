﻿using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
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

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for DetailsHitAttackTemplateUserControl.xaml
    /// </summary>
    public partial class DetailsListHitAttackTemplateUserControl : UserControl
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public DetailsListHitAttackTemplateUserControl()
        {
            InitializeComponent();
            Loaded += DetailsListHitAttackTemplateUserControl_Loaded;
            DataContextChanged += DetailsListHitAttackTemplateUserControl_DataContextChanged;

        }

        private void DetailsListHitAttackTemplateUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AttackListControl.ItemsSource = data_context.HitAttacks;
            DetailControl.DataContext = null;
        }

        private void DetailsListHitAttackTemplateUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (data_context != null)
                AttackListControl.ItemsSource = data_context.HitAttacks;
        }

        private void AttackListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AttackListControl.SelectedItem != null)
                DetailControl.DataContext = AttackListControl.SelectedItem;
        }
    }
}
