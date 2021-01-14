using DDFight.Tools.UXShortcuts;
using System;
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
                catch (Exception e) { return null; }
            }
        }

        public DamageTemplateRollableUserControl()
        {
            InitializeComponent();
        }

        public void RollControl()
        {
            if (data_context != null)
                if (data_context.Damage.LastRoll == 0)
                    data_context.Damage.Roll();
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
