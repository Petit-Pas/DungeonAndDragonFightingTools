using DDFight.Game.Aggression;
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

        /// <summary>
        ///     tells wheter the damage are applied with a crit
        /// </summary>
        public bool Crits
        {
            get { return (bool)this.GetValue(CritProperty); }
            set { this.SetValue(CritProperty, value); }
        }
        public static readonly DependencyProperty CritProperty = DependencyProperty.Register(
          "Crits", typeof(bool), typeof(DamageTemplateListRollableUserControl), new PropertyMetadata(false));

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
