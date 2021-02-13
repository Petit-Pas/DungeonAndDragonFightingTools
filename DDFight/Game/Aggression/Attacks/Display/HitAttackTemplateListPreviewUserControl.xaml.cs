using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Entities;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for DetailsHitAttackTemplateUserControl.xaml
    /// </summary>
    public partial class HitAttackTemplateListPreviewUserControl : UserControl
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public HitAttackTemplateListPreviewUserControl()
        {
            InitializeComponent();
            Loaded += DetailsListHitAttackTemplateUserControl_Loaded;
            DataContextChanged += DetailsListHitAttackTemplateUserControl_DataContextChanged;
        }

        private void DetailsListHitAttackTemplateUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AttackListControl.ItemsSource = data_context.HitAttacks;
            DetailControl.DataContext = null;
            DetailControl.Visibility = Visibility.Collapsed;
            data_context.PropertyChanged += Data_context_PropertyChanged;
        }

        private void Data_context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HitAttacks")
                AttackListControl.ItemsSource = data_context.HitAttacks;
        }

        private void DetailsListHitAttackTemplateUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (data_context != null)
                AttackListControl.ItemsSource = data_context.HitAttacks;
        }

        private void AttackListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailControl.Visibility = Visibility.Collapsed;
            if (AttackListControl.SelectedItem != null)
            {
                DetailControl.DataContext = AttackListControl.SelectedItem;
                DetailControl.Visibility = Visibility.Visible;
            }
        }

        private void AttackListControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AttackListControl.SelectedIndex != -1)
            {
                ((HitAttackTemplate)AttackListControl.SelectedItem).ExecuteAttack();
            }
        }
    }
}
