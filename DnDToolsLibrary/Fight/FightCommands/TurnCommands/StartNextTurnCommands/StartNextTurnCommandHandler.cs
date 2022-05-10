using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands
{
    public class StartNextTurnCommandHandler : SuperCommandHandlerBase<StartNextTurnCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IFightManager> _lazyFighterProvider =
            new(DIContainer.GetImplementation<IFightManager>);

        private static IFightManager _fightManager = _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(StartNextTurnCommand genericCommand)
        {
            return default;
        }
    }
}
