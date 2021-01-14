using DDFight.Game.Aggression;
using DDFight.Tools.UXShortcuts;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks.Display
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

            DataContextChanged += DamageTemplateListRollableUserControl_DataContextChanged;
        }

        private void DamageTemplateListRollableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                DamageControl.ItemsSource = data_context;
            }
            catch { }
        }
    }
}
