﻿using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputAttackSpellResults;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputSpellAttackResults
{
    /// <summary>
    /// Logique d'interaction pour GetInputSpellAttackResultsWindow.xaml
    /// </summary>
    public partial class GetInputSpellAttackResultsWindow : Window, IResultWindow<GetInputAttackSpellResultsCommand, GetInputAttackSpellResultsResponse>
    {
        public GetInputSpellAttackResultsWindow()
        {
            InitializeComponent();
        }

        private GetInputAttackSpellResultsCommand data_context {
            get => DataContext as GetInputAttackSpellResultsCommand;
        }

        public bool Validated { get; set; } = false;

        public GetInputAttackSpellResultsResponse GetResult()
        {
            return new GetInputAttackSpellResultsResponse(data_context.SpellResults);
        }

        public void LoadContext(GetInputAttackSpellResultsCommand context)
        {
            this.DataContext = context;
        }

        private void refresh_button()
        {
            CastButton.IsEnabled = false;
            if (this.AreAllChildrenValid() && this.AreAllRollableChildrenRolled())
                CastButton.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this ?", Validated = false };
                win.ShowCentered();

                if (!win.Validated)
                {
                    e.Cancel = true;
                }
            }
        }

        private void CastButton_Click(object sender, RoutedEventArgs e)
        {
            this.Validated = true;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                e.Handled = true;
                if (!this.AreAllRollableChildrenRolled())
                    this.RollRollableChildren();
            }
        }

        private void Window_Error(object sender, ValidationErrorEventArgs e)
        {
            refresh_button();
        }

        private void Window_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            refresh_button();
        }

        private void Window_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            refresh_button();
        }
    }
}
