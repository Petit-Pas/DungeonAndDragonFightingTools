using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Fight.Events;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommandHandler : SuperDndCommandHandlerBase<RemoveFighterCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(RemoveFighterCommand removeFighterCommand)
        {
            if (!FightersProvider.Contains(removeFighterCommand.Entity.DisplayName))
            {
                Console.WriteLine($"ERROR: trying to remove an entity that was not present in the fight: {removeFighterCommand.Entity.GetType()} of name {removeFighterCommand.Entity.DisplayName}");
                return MediatorCommandStatii.Error;
            }

            // fight was not started, only removing the character in this case
            if (TurnManager.TurnIndex == -1)
            {
                FightersProvider.RemoveFighter(removeFighterCommand.Entity);
                return MediatorCommandStatii.Success;
            }

            var wasPlaying = TurnManager.TurnIndex == removeFighterCommand.Entity.TurnOrder;

            if (wasPlaying)
            {
                var startNextTurnCommand = new StartNextTurnCommand();
                Mediator.Execute(startNextTurnCommand);
                removeFighterCommand.PushToInnerCommands(startNextTurnCommand);
            }

            FightersProvider.RemoveFighter(removeFighterCommand.Entity);

            TurnManager.SetTurnOrders();

            FightersProvider.InvokeFighterSelected(new FighterSelectedEventArgs(FightersProvider.CurrentlyPlaying.DisplayName));


            return MediatorCommandStatii.Success;
        }

        // TODO what if a monster with the same name was added since then ?
        // TODO what about the numbers they have in their display name ?
        public override void Undo(RemoveFighterCommand genericCommand)
        {
            FightersProvider.AddFighter(genericCommand.Entity);
            TurnManager.SetTurnOrders();
            base.Undo(genericCommand);
        }
    }
}
