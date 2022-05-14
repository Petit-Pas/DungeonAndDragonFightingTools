using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseRoundCommands;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseTurnCommands
{
    public class IncreaseTurnCommandHandler : SuperCommandHandlerBase<IncreaseTurnCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>);
        private static ITurnManager _turnManager = _lazyTurnManager.Value;

        private static readonly Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private static IFightersProvider _fightersProvider = _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(IncreaseTurnCommand increaseTurnCommand)
        {
            _turnManager.TurnIndex += 1;
            if (_turnManager.TurnIndex == _fightersProvider.FighterCount)
            {
                var increaseRoundCommand = new IncreaseRoundCommand();
                _mediator.Value.Execute(increaseRoundCommand);
                increaseTurnCommand.PushToInnerCommands(increaseRoundCommand);
            }

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IncreaseTurnCommand command)
        {
            base.Undo(command);
            _turnManager.TurnIndex -= 1;
        }
    }
}
