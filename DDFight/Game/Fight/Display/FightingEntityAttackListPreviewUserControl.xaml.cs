using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Entities;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Fight.Display
{
    /// <summary>
    /// Interaction logic for DetailsHitAttackTemplateUserControl.xaml
    /// </summary>
    public partial class FightingEntityAttackListPreviewUserControl : UserControl
    {
        public FightingEntityAttackListPreviewUserControl()
        {
            DataContextChanged += DetailsListHitAttackTemplateUserControl_DataContextChanged;
            InitializeComponent();
            HitAttackTemplateListControl.EntityListControl.SelectionChanged += AttackListControl_SelectionChanged;
        }

        private void DetailsListHitAttackTemplateUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DetailControl.DataContext = null;
            DetailControl.Visibility = Visibility.Collapsed;
        }

        private void AttackListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailControl.Visibility = Visibility.Collapsed;
            if (HitAttackTemplateListControl.EntityListControl.SelectedIndex != -1)
            {
                DetailControl.DataContext = HitAttackTemplateListControl.EntityListControl.SelectedItem;
                DetailControl.Visibility = Visibility.Visible;
            }
        }
    }
}
