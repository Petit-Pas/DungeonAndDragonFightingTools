using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands
{
    public class StartNextTurnCommandHandler : SuperCommandHandlerBase<StartNextTurnCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IFightersProvider> _lazyFighterProvider =
            new(DIContainer.GetImplementation<IFightersProvider>);

        private static IFightersProvider _fightersProvider = _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(StartNextTurnCommand genericCommand)
        {
            return default;
        }
    }
}
