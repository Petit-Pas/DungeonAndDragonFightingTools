using DDFight.Tools;
using DDFight.Tools.UXShortcuts;
using DDFight.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellAttackCastWindow.xaml
    /// </summary>
    public partial class SpellAttackCastWindow : Window, IRollableControl
    {
        public SpellAttackCastWindow()
        {
            InitializeComponent();
        }

        public bool IsFullyRolled()
        {
            return RollableWindowTool.AreAllRollableChildrenRolled(this);
        }

        public void RollControl()
        {
            RollableWindowTool.RollRollableChildren(this);
        }

        private void refresh_CastButton()
        {
            CastButtonControl.IsEnabled = false;
            if (this.AreAllChildrenValid())
                CastButtonControl.IsEnabled = true;
        }

        private AttackSpellResult data_context
        {
            get => (AttackSpellResult)DataContext;
        }

        private void CastButtonControl_Click(object sender, RoutedEventArgs e)
        {
            List<SpellAttackResultRollableUserControl> attacks = new List<SpellAttackResultRollableUserControl>();
            foreach (SpellAttackResultRollableUserControl control in this.GetAllChildrenByName("SpellAttackResultRollableUserControl"))
            {
                attacks.Add(control);
            }

            data_context.Cast(attacks);

            validated = true;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                e.Handled = true;
                if (!IsFullyRolled())
                    RollControl();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            refresh_CastButton();
        }

        private bool validated = false;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (validated == false)
            {
                AskYesNoWindow askYesNoWindow = new AskYesNoWindow() 
                {
                    DataContext = new AskYesNoDataContext 
                    { 
                        Message = "Are you sure you wish to cancel this spell?"
                    }
                };
                askYesNoWindow.ShowCentered();

                if (((AskYesNoDataContext)askYesNoWindow.DataContext).Yes == false)
                    e.Cancel = true;
            }
        }
    }
}
