using DDFight.Controlers.InputBoxes;
using DDFight.Game.Aggression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DDFight.Controlers.Game.Attacks.DamageListControls
{
    /// <summary>
    /// Logique d'interaction pour PlayableDamageListUserControl.xaml
    /// </summary>
    public partial class DamageTemplateListRollableUserControl : UserControl
    {
        private List<DamageTemplate> data_context
        {
            get => (List<DamageTemplate>)DataContext;
        }

        public DamageTemplateListRollableUserControl()
        {
            InitializeComponent();

            Loaded += PlayableDamageListUserControl_Loaded;
        }

        private void PlayableDamageListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DamageControl.ItemsSource = data_context;
        }
    }
}
