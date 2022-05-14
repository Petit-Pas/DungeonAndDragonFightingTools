using System;
using System.Windows;
using System.Windows.Controls;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DDFight.Tools;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands;
using WpfToolsLibrary.Extensions;

namespace DDFight.Game.Fight.Display
{
    /// <summary>
    /// Interaction logic pour GeneralInfoFightUserControl.xaml
    /// </summary>
    public partial class FightGeneralInfoUserControl : UserControl, IEventUnregisterable
    {
        private static readonly Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private static readonly IFightersProvider _fightersProvider = _lazyFighterProvider.Value;

        private static readonly Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>());
        private static ITurnManager _turnManager => _lazyTurnManager.Value;

        private static readonly Lazy<IMediator> _lazyMediator = new(DIContainer.GetImplementation<IMediator>());
        private static IMediator _mediator => _lazyMediator.Value;


        public FightGeneralInfoUserControl()
        {
            InitializeComponent();
            Loaded += GeneralInfoFightUserControl_Loaded;
        }

        private void SetCharactersTurnName()
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + _fightersProvider.GetFighterByIndex((int)_turnManager.TurnIndex).DisplayName;
        }

        private void GeneralInfoFightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _turnManager;
            _turnManager.TurnStarted += FightContext_NewTurnStarted;
            SetCharactersTurnName();
        }

        private void FightContext_NewTurnStarted(object sender, TurnStartedEventArgs args)
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + args.EntityName;
        }

        public void UnregisterToAll()
        {
            _turnManager.TurnStarted -= FightContext_NewTurnStarted;
        }

        private void AddToFightButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO this should probably be enhanced
            var currentlyPlaying = _fightersProvider.GetFighterByIndex(_turnManager.TurnIndex);

            var window = new AddToFightWindow();
             
            window.ShowCentered();

            var rollInitiativeWindow = new RollInitiativeWindow
            {
                // TODO the window should probably not take anything in parameter account, and this could be made through a command
                DataContext = _fightersProvider
            };

            rollInitiativeWindow.ShowCentered();

            _turnManager.SetTurnOrders();

            for (var i = 0; i != _fightersProvider.FighterCount; i++)
            {
                if (_fightersProvider.GetFighterByIndex(i) == currentlyPlaying)
                {
                    _turnManager.TurnIndex = i;
                }
            }

        }

        private void NextTurnButton_Click(object sender, RoutedEventArgs e)
        {
            var startNextTurnCommand = new StartNextTurnCommand();
            _mediator.Execute(startNextTurnCommand);
        }
    }
}
