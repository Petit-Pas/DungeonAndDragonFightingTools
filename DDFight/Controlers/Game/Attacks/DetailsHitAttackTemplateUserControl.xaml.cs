using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
using DDFight.Windows.FightWindows;
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
    public partial class DetailsHitAttackTemplateUserControl : UserControl
    {
        public DetailsHitAttackTemplateUserControl()
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
