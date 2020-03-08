using DDFight.Game.Aggression.Attacks;
using DDFight.Windows.FightWindows;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for DetailsHitAttackTemplateUserControl.xaml
    /// </summary>
    public partial class HitAttackTemplateDetailsPreviewUserControl : UserControl
    {
        public HitAttackTemplateDetailsPreviewUserControl()
        {
            InitializeComponent();
            Loaded += DetailsHitAttackTemplateUserControl_Loaded;
        }

        private void DetailsHitAttackTemplateUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContextChanged += DetailsHitAttackTemplateUserControl_DataContextChanged;
        }

        private void DetailsHitAttackTemplateUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(DataContext is HitAttackTemplate))
                this.Visibility = Visibility.Hidden;
            else
                this.Visibility = Visibility.Visible;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteHitAttackWindow window = new ExecuteHitAttackWindow()
            {
                DataContext = this.DataContext,
                Owner = Window.GetWindow(this),
            };

            window.ShowDialog();

        }
    }
}
