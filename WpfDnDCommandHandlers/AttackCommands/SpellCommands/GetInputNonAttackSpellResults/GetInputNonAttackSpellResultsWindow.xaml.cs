﻿using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputNonAttackSpellResults;
using DnDToolsLibrary.Attacks.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputNonAttackSpellResults
{
    /// <summary>
    /// Logique d'interaction pour GetInputSpellNonAttackResultsWindow.xaml
    /// </summary>
    public partial class GetInputNonAttackSpellResultsWindow : Window, IResultWindow<GetInputNonAttackSpellResultsCommand, GetInputNonAttackSpellResultsResponse>
    {
        public GetInputNonAttackSpellResultsWindow()
        {
            InitializeComponent();
        }

        private GetInputNonAttackSpellResultsCommand data_context { get => DataContext as GetInputNonAttackSpellResultsCommand; }

        public bool Validated { get; set; } = false;

        public GetInputNonAttackSpellResultsResponse GetResult()
        {
            return new GetInputNonAttackSpellResultsResponse(data_context.SpellResults);
        }

        public void LoadContext(GetInputNonAttackSpellResultsCommand context)
        {
            DataContext = context;
            refresh_button();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this spell?", Validated = false };
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

        private void refresh_button()
        {
            CastButton.IsEnabled = false;
            if (this.AreAllChildrenValid() && this.AreAllRollableChildrenRolled())
                CastButton.IsEnabled = true;
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
