using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Tools;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Logique d'interaction pour TakeDamageWindow.xaml
    /// </summary>
    public partial class TakeDamageWindow : Window
    {

        private PlayableEntity data_context {
            get => (PlayableEntity)DataContext;
        }

        List<DamageTemplate> damage_list = new List<DamageTemplate>();

        public TakeDamageWindow()
        {
            InitializeComponent();
            
            Loaded += TakeDamageWindow_Loaded;
        }

        private void TakeDamageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DamageControl.DataContext = damage_list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                ErrorControl.Visibility = Visibility.Visible;
                return;
            }

            if (damage_list.Count != 0)
            {
                foreach (DamageTemplate dmg in damage_list)
                {
                    dmg.Damage.Roll();
                }
                data_context.TakeHitDamage(damage_list);
            }
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
