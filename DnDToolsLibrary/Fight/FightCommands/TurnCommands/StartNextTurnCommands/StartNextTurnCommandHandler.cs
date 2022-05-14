using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseTurnCommands;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands
{
    public class StartNextTurnCommandHandler : SuperCommandHandlerBase<StartNextTurnCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private static IFightersProvider _fightersProvider = _lazyFighterProvider.Value;

        private static readonly Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>);
        private static ITurnManager _turnManager = _lazyTurnManager.Value;


        public override IMediatorCommandResponse Execute(StartNextTurnCommand startNextTurnCommand)
        {
            EndTurn(startNextTurnCommand);

            IncreaseTurn(startNextTurnCommand);

            StartTurn(startNextTurnCommand);

            return MediatorCommandStatii.NoResponse;
        }

        private void EndTurn(StartNextTurnCommand command)
        {
            // can only end turn if this is not the 1st turn of the fight
            if (_turnManager.TurnIndex != -1)
            {
                var endingFighter = _fightersProvider.GetFighterByIndex(_turnManager.TurnIndex);
                var endTurnCommand = new EndTurnCommand(endingFighter.DisplayName);
                _mediator.Value.Execute(endTurnCommand);
                command.PushToInnerCommands(endTurnCommand);
            }
        }

        private void IncreaseTurn(StartNextTurnCommand startNextTurnCommand)
        {
            var increaseTurnCommand = new IncreaseTurnCommand();
            _mediator.Value.Execute(increaseTurnCommand);
            startNextTurnCommand.PushToInnerCommands(increaseTurnCommand);
        }

        private void StartTurn(StartNextTurnCommand command)
        {
            var startingFighter = _fightersProvider.GetFighterByIndex(_turnManager.TurnIndex);
            var startTurnCommand = new StartTurnCommand(startingFighter.DisplayName);
            _mediator.Value.Execute(startTurnCommand);
            command.PushToInnerCommands(startTurnCommand);
        }
    }
}
