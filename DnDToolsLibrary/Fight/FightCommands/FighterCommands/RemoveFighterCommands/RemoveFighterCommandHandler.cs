using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommandHandler : BaseMediatorHandler<RemoveFighterCommand, IMediatorCommandResponse>
    {
        private Lazy<IFighterProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFighterProvider>);
        private IFighterProvider _fighterProvider => _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(RemoveFighterCommand genericCommand)
        {
            if (_fighterProvider.Remove(genericCommand.Entity))
            {
                return MediatorCommandStatii.Success;
            }

            Console.WriteLine($"ERROR: trying to remove an entity that was not present in the fight: {genericCommand.Entity.GetType()} of name {genericCommand.Entity.DisplayName}");
            return MediatorCommandStatii.Error;
        }

        public override void Undo(RemoveFighterCommand genericCommand)
        {
            _fighterProvider.AddFighter(genericCommand.Entity);
        }
    }
}
