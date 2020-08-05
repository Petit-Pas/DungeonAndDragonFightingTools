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
