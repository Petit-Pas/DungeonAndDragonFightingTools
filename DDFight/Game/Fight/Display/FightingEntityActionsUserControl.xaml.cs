using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using DnDToolsLibrary.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour FighterActionUserControl.xaml
    /// </summary>
    public partial class FightingEntityActionsUserControl : UserControl, IEventUnregisterable
    {

        public PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }


        // TODO events handled in a weird way
        public FightingEntityActionsUserControl()
        {
            DataContext = null;
            InitializeComponent();
            Loaded += FighterActionUserControl_Loaded;
        }

        private void FighterActionUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalContext.Context.FightContext.CharacterSelected += FightContext_CharacterSelected;
            this.LayoutUpdated += FighterActionUserControl_LayoutUpdated;
        }

        private void FighterActionUserControl_LayoutUpdated(object sender, EventArgs e)
        {
            DataContext = GlobalContext.Context.FightContext.FightersList.Elements.ElementAt(0);
            this.LayoutUpdated -= FighterActionUserControl_LayoutUpdated;
        }

        private void FightContext_CharacterSelected(object sender, SelectedCharacterEventArgs args)
        {
            DataContext = args.Character;
        }

        public void UnregisterToAll()
        {
            GlobalContext.Context.FightContext.CharacterSelected -= FightContext_CharacterSelected;
        }

    }
}
