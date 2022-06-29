using System;
using System.Linq;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight.Events;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommandHandler : SuperDndCommandHandlerBase<RemoveFighterCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(RemoveFighterCommand removeFighterCommand)
        {
            var entity = removeFighterCommand.Entity;

            if (!FightersProvider.Contains(entity.DisplayName))
            {
                Console.WriteLine($"ERROR: trying to remove an entity that was not present in the fight: {entity.GetType()} of name {entity.DisplayName}");
                return MediatorCommandStatii.Error;
            }

            // fight was not started, only removing the character in this case
            if (TurnManager.TurnIndex == -1)
            {
                FightersProvider.RemoveFighter(entity);
                return MediatorCommandStatii.Success;
            }

            var wasPlaying = TurnManager.TurnIndex == entity.TurnOrder;

            if (wasPlaying)
            {
                var startNextTurnCommand = new StartNextTurnCommand();
                Mediator.Execute(startNextTurnCommand);
                removeFighterCommand.PushToInnerCommands(startNextTurnCommand);
            }

            // TODO this is not unit tested
            foreach (var status in entity.AffectingStatusList.ToArray())
            {
                var removeStatusCommand = new RemoveStatusCommand(status.ActualStatusReferenceId, entity.DisplayName);
                Mediator.Execute(removeStatusCommand);
                removeFighterCommand.PushToInnerCommands(removeStatusCommand);
            }

            FightersProvider.RemoveFighter(removeFighterCommand.Entity);

            TurnManager.SetTurnOrders();

            FightersProvider.InvokeFighterSelected(new FighterSelectedEventArgs(FightersProvider.CurrentlyPlaying.DisplayName));


            return MediatorCommandStatii.Success;
        }

        // TODO what if a monster with the same name was added since then ?
        // TODO what about the numbers they have in their display name ?
        public override void Undo(RemoveFighterCommand command)
        {
            base.Undo(command);

            FightersProvider.AddFighter(command.Entity);
            TurnManager.SetTurnOrders();
            base.Undo(command);
        }
    }
}
