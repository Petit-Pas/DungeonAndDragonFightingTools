using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight.Events;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommandHandler : SuperCommandHandlerBase<RemoveFighterCommand, IMediatorCommandResponse>
    {
        private Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private IFightersProvider _fightersProvider => _lazyFighterProvider.Value;

        private Lazy<ITurnManager> _lazyTurnManager = new(() => DIContainer.GetImplementation<ITurnManager>());
        private ITurnManager _turnManager => _lazyTurnManager.Value;

        public override IMediatorCommandResponse Execute(RemoveFighterCommand removeFighterCommand)
        {
            if (!_fightersProvider.Contains(removeFighterCommand.Entity.DisplayName))
            {
                Console.WriteLine($"ERROR: trying to remove an entity that was not present in the fight: {removeFighterCommand.Entity.GetType()} of name {removeFighterCommand.Entity.DisplayName}");
                return MediatorCommandStatii.Error;
            }

            // fight was not started, only removing the character in this case
            if (_turnManager.TurnIndex == -1)
            {
                _fightersProvider.RemoveFighter(removeFighterCommand.Entity);
                return MediatorCommandStatii.Success;
            }

            var wasPlaying = _turnManager.TurnIndex == removeFighterCommand.Entity.TurnOrder;

            if (wasPlaying)
            {
                var startNextTurnCommand = new StartNextTurnCommand();
                _mediator.Value.Execute(startNextTurnCommand);
                removeFighterCommand.PushToInnerCommands(startNextTurnCommand);
            }

            _fightersProvider.RemoveFighter(removeFighterCommand.Entity);

            _turnManager.SetTurnOrders();

            _fightersProvider.InvokeFighterSelected(new FighterSelectedEventArgs(_fightersProvider.CurrentlyPlaying.DisplayName));


            return MediatorCommandStatii.Success;
        }

        // TODO what if a monster with the same name was added since then ?
        // TODO what about the numbers they have in their display name ?
        public override void Undo(RemoveFighterCommand genericCommand)
        {
            _fightersProvider.AddFighter(genericCommand.Entity);
            _turnManager.SetTurnOrders();
            base.Undo(genericCommand);
        }
    }
}
