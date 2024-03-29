﻿using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries;
using System;
using System.Windows;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellLevelQueries.CantripLevelQueries
{ 
    /// <summary>
    /// Logique d'interaction pour CantripLevelSelectorWindow.xaml
    /// </summary>
    public partial class CantripLevelQueryWindow : Window, IResultWindow<DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries.CantripLevelQuery, SpellLevel>
    {
        public CantripLevelQueryWindow() 
        {
            Initialized += NormalSpellLevelSelectorWindow_Initialized;
            InitializeComponent();
        }

        private void NormalSpellLevelSelectorWindow_Initialized(object sender, EventArgs e)
        {
            CircularSelector.SelectedLevelChanged += CircularSelector_SelectedLevelChanged;
        }

        private void CircularSelector_SelectedLevelChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            ValidateButton.IsEnabled = false;
            if (CircularSelector.SelectedLevel >= 1 && CircularSelector.SelectedLevel <= 9)
            {
                ValidateButton.IsEnabled = true;
            }
        }

        public bool Validated { get; set; } = false;

        public int SelectedLevel { get; set; }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedLevel = CircularSelector.SelectedLevel;
            this.Validated = true;
            this.Close();
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

        public SpellLevel GetResult()
        {
            return new SpellLevel(SelectedLevel);
        }

        public void LoadContext(CantripLevelQuery context)
        {
        }
    }
}
