using System;
using System.Windows;
using System.Windows.Controls;
using BaseToolsLibrary.DependencyInjection;
using DDFight.Tools;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;
using WpfToolsLibrary.Extensions;

namespace DDFight.Game.Fight.Display
{
    /// <summary>
    /// Interaction logic pour GeneralInfoFightUserControl.xaml
    /// </summary>
    public partial class FightGeneralInfoUserControl : UserControl, IEventUnregisterable
    {
        private static readonly Lazy<IFighterProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFighterProvider>);
        private static readonly IFighterProvider _fighterProvider = _lazyFighterProvider.Value;


        public FightGeneralInfoUserControl()
        {
            InitializeComponent();
            Loaded += GeneralInfoFightUserControl_Loaded;
        }

        private void SetCharactersTurnName()
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + _fighterProvider.ElementAt((int)GlobalContext.Context.FightContext.TurnIndex).DisplayName;
        }

        private void GeneralInfoFightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalContext.Context.FightContext.NewTurnStarted += FightContext_NewTurnStarted;
            SetCharactersTurnName();
        }

        private void FightContext_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + args.EntityName;
        }

        public void UnregisterToAll()
        {
            GlobalContext.Context.FightContext.NewTurnStarted -= FightContext_NewTurnStarted;
        }

        private void AddToFightButton_Click(object sender, RoutedEventArgs e)
        {
            var currentlyPlaying = GlobalContext.Context.FightContext.CurrentlyPlaying;

            var window = new AddToFightWindow();
             
            window.ShowCentered();

            var rollInitiativeWindow = new RollInitiativeWindow
            {
                // TODO the window should probably not take anything into account, and this could be made through a command
                DataContext = _fighterProvider
            };

            rollInitiativeWindow.ShowCentered();

            GlobalContext.Context.FightContext.FightersList.SetTurnOrdersMiddleFight();

            for (var i = 0; i != GlobalContext.Context.FightContext.FightersList.Count; i++)
            {
                if (GlobalContext.Context.FightContext.FightersList[i] == currentlyPlaying)
                {
                    GlobalContext.Context.FightContext.TurnIndex = i;
                }
            }

        }

        private void NextTurnButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalContext.Context.FightContext.NextTurn();
        }
    }
}
