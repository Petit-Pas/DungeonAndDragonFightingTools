using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseRoundCommands
{
    public class IncreaseRoundCommandHandler : BaseMediatorHandler<IncreaseRoundCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>);
        private static ITurnManager _turnManager = _lazyTurnManager.Value;
        
        private static readonly Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private static IFightersProvider _fightersProvider = _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(IncreaseRoundCommand genericCommand)
        {
            if (_turnManager.TurnIndex != _fightersProvider.FighterCount)
            {
                return MediatorCommandStatii.Canceled;
            }
            _turnManager.RoundCount += 1;
            _turnManager.TurnIndex = 0;
            return MediatorCommandStatii.Success;
        }

        public override void Undo(IncreaseRoundCommand genericCommand)
        {
            _turnManager.TurnIndex = _fightersProvider.FighterCount;
            _turnManager.RoundCount -= 1;
        }
    }
}
