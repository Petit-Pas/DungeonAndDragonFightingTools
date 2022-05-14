using DDFight.Tools;
using DnDToolsLibrary.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour FighterActionUserControl.xaml
    /// </summary>
    public partial class FightingEntityActionsUserControl : UserControl, IEventUnregisterable
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>());
        protected static IFightersProvider _fightersProvider => _lazyFightManager.Value;


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
            _fightersProvider.FighterSelected += FightContext_CharacterSelected;
            this.LayoutUpdated += FighterActionUserControl_LayoutUpdated;
        }

        private void FighterActionUserControl_LayoutUpdated(object sender, EventArgs e)
        {
            DataContext = _fightersProvider.First();
            this.LayoutUpdated -= FighterActionUserControl_LayoutUpdated;
        }

        private void FightContext_CharacterSelected(object sender, FighterSelectedEventArgs args)
        {
            DataContext = _fightersProvider.GetFighterByDisplayName(args.EntityName);
        }

        public void UnregisterToAll()
        {
            _fightersProvider.FighterSelected -= FightContext_CharacterSelected;
        }

    }
}
