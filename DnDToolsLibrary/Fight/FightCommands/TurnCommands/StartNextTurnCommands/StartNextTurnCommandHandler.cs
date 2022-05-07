using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands
{
    public class StartNextTurnCommandHandler : SuperCommandHandlerBase<StartNextTurnCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IFighterProvider> _lazyFighterProvider =
            new(DIContainer.GetImplementation<IFighterProvider>);

        private static IFighterProvider _fighterProvider = _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(StartNextTurnCommand genericCommand)
        {
            return default;
        }
    }
}
