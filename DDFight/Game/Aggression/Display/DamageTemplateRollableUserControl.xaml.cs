using DDFight.Tools.UXShortcuts;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Logique d'interaction pour DamageTemplateRollableUserControl.xaml
    /// </summary>
    public partial class DamageTemplateRollableUserControl : UserControl, IRollableControl
    {
        private DamageTemplate data_context
        {
            get
            {
                try { return (DamageTemplate)DataContext; }
                catch (Exception) { return null; }
            }
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
          "Crits", typeof(bool), typeof(DamageTemplateRollableUserControl), new PropertyMetadata(false));

        public DamageTemplateRollableUserControl()
        {
            InitializeComponent();
        }

        public void RollControl()
        {
            if (data_context != null)
                if (data_context.Damage.LastRoll == 0)
                    data_context.Damage.Roll(Crits);
        }

        public bool IsFullyRolled()
        {
            if (data_context != null)
                if (data_context.Damage.LastRoll == 0)
                    return false;
            return true;
        }
    }
}
