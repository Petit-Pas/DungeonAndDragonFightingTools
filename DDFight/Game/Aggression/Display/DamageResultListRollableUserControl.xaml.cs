using DDFight.Game.Aggression;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks.Display
{
    /// <summary>
    /// Logique d'interaction pour DamageResultListRollableUserControl.xaml
    /// </summary>
    public partial class DamageResultListRollableUserControl : UserControl
    {
        private DamageResultList data_context
        {
            get => DataContext as DamageResultList;
        }

        public DamageResultListRollableUserControl()
        {
            DataContextChanged += DamageResultListRollableUserControl_DataContextChanged;
            InitializeComponent();
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
          "Crits", typeof(bool), typeof(DamageResultListRollableUserControl), new PropertyMetadata(false));

        private void DamageResultListRollableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            /*if (data_context != null)
                DamageControl.ItemsSource = data_context.Elements;*/
        }
    }
}
