using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands
{
    public class RemoveReactionCommandHandler : DndCommandHandlerBase<RemoveReactionCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(RemoveReactionCommand command)
        {
            var target = command.GetEntity();

            if (target.HasReaction)
            {
                target.HasReaction = false;
                command.CommandStatus = MediatorCommandStatii.Success;
            }
            else
            {
                Console.WriteLine($"WARN: trying to remove action for {target.DisplayName} while he has none");
                command.CommandStatus = MediatorCommandStatii.Canceled;
            }

            return command.CommandStatus;
        }

        public override void Undo(RemoveReactionCommand command)
        {
            base.Undo(command);
            if (command.CommandStatus == MediatorCommandStatii.Success)
            {
                command.GetEntity().HasReaction = true;
            }
        }
    }
}