using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks.DamageListControls
{
    /// <summary>
    /// Logique d'interaction pour PlayableDamageListUserControl.xaml
    /// </summary>
    public partial class DamageTemplateListRollableUserControl : UserControl
    {


        public DamageTemplateListRollableUserControl()
        {
            InitializeComponent();

            Loaded += PlayableDamageListUserControl_Loaded;
        }

        private void PlayableDamageListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DamageControl.ItemsSource = (System.Collections.IEnumerable)DataContext;
        }
    }
}
