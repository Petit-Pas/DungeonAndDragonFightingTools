using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommandHandler : BaseMediatorHandler<RemoveFighterCommand, IMediatorCommandResponse>
    {
        private Lazy<IFightManager> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightManager>);
        private IFightManager _fightManager => _lazyFighterProvider.Value;

        public override IMediatorCommandResponse Execute(RemoveFighterCommand genericCommand)
        {
            if (_fightManager.RemoveFighter(genericCommand.Entity))
            {
                return MediatorCommandStatii.Success;
            }

            Console.WriteLine($"ERROR: trying to remove an entity that was not present in the fight: {genericCommand.Entity.GetType()} of name {genericCommand.Entity.DisplayName}");
            return MediatorCommandStatii.Error;
        }

        // TODO what if a monster with the same name was added since then ?
        // TODO what about the numbers they have in their display name ?
        public override void Undo(RemoveFighterCommand genericCommand)
        {
            _fightManager.AddFighter(genericCommand.Entity);
        }
    }
}
