﻿using DDFight.Game;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour FighterActionUserControl.xaml
    /// </summary>
    public partial class FighterActionUserControl : UserControl, IEventUnregisterable
    {

        public PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public FighterActionUserControl()
        {
            DataContext = null;
            InitializeComponent();
            Loaded += FighterActionUserControl_Loaded;
        }

        private void FighterActionUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Global.Context.FightContext.CharacterSelected += FightContext_CharacterSelected;
            this.LayoutUpdated += FighterActionUserControl_LayoutUpdated;
            //AttacksListControl.DataContext = data_context;
        }

        private void FighterActionUserControl_LayoutUpdated(object sender, EventArgs e)
        {
            DataContext = Global.Context.FightContext.FightersList.Fighters.ElementAt(0);
            this.LayoutUpdated -= FighterActionUserControl_LayoutUpdated;
        }

        private void FightContext_CharacterSelected(object sender, SelectedCharacterEventArgs args)
        {
            NextTurnButton.IsEnabled = false;
            if (Global.Context.FightContext.CurrentlyPlaying == args.Character)
                NextTurnButton.IsEnabled = true;
            DataContext = args.Character;
        }

        private void NextTurn_Button_Click(object sender, RoutedEventArgs e)
        {
            Global.Context.FightContext.NextTurn();
        }

        public void UnregisterToAll()
        {
            Global.Context.FightContext.CharacterSelected -= FightContext_CharacterSelected;
        }

    }
}
