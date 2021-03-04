using DDFight.Game.Aggression;
using DDFight.Tools.UXShortcuts;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks.Display
{
    /// <summary>
    /// Logique d'interaction pour DamageTemplateRollableUserControl.xaml
    /// </summary>
    public partial class DamageResultRollableUserControl : UserControl, IRollableControl
    {
        private DamageResult data_context
        {
            get => DataContext as DamageResult;
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
          "Crits", typeof(bool), typeof(DamageResultRollableUserControl), 
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public DamageResultRollableUserControl()
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
