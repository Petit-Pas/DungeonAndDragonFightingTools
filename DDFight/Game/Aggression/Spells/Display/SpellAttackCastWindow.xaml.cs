﻿using DDFight.Windows;
using DnDToolsLibrary.Attacks.Spells;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TempExtensionsAttackSpellResultExtensions;
using WpfDnDCustomControlLibrary.Attacks.Spells;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

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
            return this.AreAllRollableChildrenRolled();
        }

        public void RollControl()
        {
            this.RollRollableChildren();
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
                attacks.Add(control);

            data_context.Cast(attacks);

            validated = true;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
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
